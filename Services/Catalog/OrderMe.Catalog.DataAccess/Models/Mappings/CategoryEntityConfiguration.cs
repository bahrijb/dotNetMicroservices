using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderMe.Catalog.DataAccess.Models.Mappings
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CATEGORY");

            builder.Property(x => x.CategoryId)
                .HasColumnName("Category_PK");

            builder.Property(x => x.ParentCategoryId)
                .HasColumnName("ParentCategory_FK")
                .IsRequired(false);

            builder.HasOne(x => x.ParentCategory)
             .WithMany(x => x.ChildCategories)
             .HasForeignKey(x => x.ParentCategoryId)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
