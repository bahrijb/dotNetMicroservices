using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderMe.Cart.DataAccess.Constants;
using OrderMe.Cart.BusinessLogic.Cart.Mappings;
using OrderMe.Cart.BusinessLogic.Cart.Services;

namespace OrderMe.Cart.Api
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
            services.Configure<CartStoreDataBaseSetings>(Configuration.GetSection("CartStoreCollection"));

            // Swagger configurations
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format("./OrderMe.Cart.Api.Swagger.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OrderMe.Cart.Api",
                });
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CartMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            // Injections for constructors
            services.AddSingleton(mapper);
            services.AddScoped<ICartService, CartService>();

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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderMe.Cart.Api");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
