using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Core.Models;

namespace Order.Logic.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder
                .Property(entity => entity.ClusteredKey)
                .HasColumnName("clustered_key")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasIndex(entity => entity.ClusteredKey)
                .IsClustered();

            builder
                .Property(entity => entity.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasKey(entity => entity.Id)
                .IsClustered(false);

            builder
                .Property(entity => entity.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(entity => entity.QOH)
                .HasColumnName("qoh")
                .IsRequired();

            builder
                .Property(entity => entity.CreatedDate)
                .HasColumnName("create_date")
                .IsRequired();

            builder
                .Property(entity => entity.ModifiedDate)
                .HasColumnName("modified_date");
        }
    }
}
