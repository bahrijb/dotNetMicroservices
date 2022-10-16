using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace OrderMe.Catalog.DataAccess.Models.Mappings
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("ITEM");

            builder.Property(x => x.ItemId)
                .HasColumnName("Item_PK");

            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .HasColumnName("Category_FK")
                .IsRequired();

            builder.HasOne(x => x.Category)
             .WithMany(x => x.Items)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
