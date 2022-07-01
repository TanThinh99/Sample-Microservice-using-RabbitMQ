using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Core.Models;

namespace Order.Logic.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

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
                .HasMaxLength(100)
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
