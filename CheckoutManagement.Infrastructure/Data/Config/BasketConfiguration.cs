using CkeckoutManagement.Core.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckoutManagement.Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Baskets").HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();

            builder.OwnsOne(b => b.Value, b =>
            {
                b.Property(bb => bb.TotalNet)
                .HasColumnName("Value_TotalNet");
                b.Property(bb => bb.TotalGross)
                .HasColumnName("Value_TotalGross");
                b.Property(bb => bb.PaysVAT)
                .HasColumnName("Value_PaysVAT");
            });
            builder.OwnsOne(b => b.Status, b =>
            {
                b.Property(bb => bb.Closed)
                .HasColumnName("Status_Closed");
                b.Property(bb => bb.Payed)
                .HasColumnName("Status_Payed");
            });
        }
    }
}
