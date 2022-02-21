using CkeckoutManagement.Core.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckoutManagement.Infrastructure.Data.Config
{
    public class ArticleLineConfiguration : IEntityTypeConfiguration<ArticleLine>
    {
        public void Configure(EntityTypeBuilder<ArticleLine> builder)
        {
            builder.ToTable("ArticleLines").HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
