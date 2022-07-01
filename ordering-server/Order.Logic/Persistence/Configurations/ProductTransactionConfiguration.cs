using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Core.Models;

namespace Order.Logic.Persistence.Configurations
{
    public class ProductTransactionConfiguration : IEntityTypeConfiguration<ProductTransaction>
    {
        public void Configure(EntityTypeBuilder<ProductTransaction> builder)
        {
            builder.ToTable(nameof(ProductTransaction));

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
                .Property(entity => entity.cId)
                .HasColumnName("c_id")
                .HasMaxLength(36)
                .IsRequired();

            builder
                .Property(entity => entity.pId)
                .HasColumnName("p_id")
                .HasMaxLength(36)
                .IsRequired();

            builder
                .Property(entity => entity.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder
                .Property(entity => entity.Status)
                .HasColumnName("status")
                .HasMaxLength(36)
                .IsRequired();

            builder
                .Property(entity => entity.Description)
                .HasColumnName("description")
                .HasMaxLength(200);

            builder
                .Property(entity => entity.OrderDate)
                .HasColumnName("order_date");

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
