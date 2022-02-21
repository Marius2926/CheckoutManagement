using CkeckoutManagement.Core.SyncedAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckoutManagement.Infrastructure.Data.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers").HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
