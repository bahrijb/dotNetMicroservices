using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderMe.Catalog.BusinessLogic.Category.Mappings;
using OrderMe.Catalog.BusinessLogic.Category.Services;
using OrderMe.Catalog.BusinessLogic.Item.Mappings;
using OrderMe.Catalog.BusinessLogic.Item.Services;
using OrderMe.Catalog.DataAccess.Contexts;
using MassTransit;
using System;

namespace OrderMe.Catalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Databaes configurations
            services.AddDbContext<CatalogDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName)));
            services.AddScoped(provider => provider.GetService<CatalogDbContext>());

            // Swagger configurations
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format("./OrderMe.Catalog.Api.Swagger.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OrderMe.Catalog.Api",
                });
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoryMapping());
                mc.AddProfile(new ItemMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            // Injections for constructors
            services.AddSingleton(mapper);
            services.AddScoped<ICatalogDbContext, CatalogDbContext>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderMe.Catalog.Api");
            });
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
