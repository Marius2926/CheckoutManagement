using Ardalis.GuardClauses;
using SharedKernel;

namespace CkeckoutManagement.Core.BasketAggregate
{
    public class ArticleLine : BaseEntity<int>
    {
        public ArticleLine(Guid basketId, string name, double price)
        {
            BasketId = Guard.Against.Default(basketId, nameof(basketId));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Price = Guard.Against.Negative(price, nameof(price));
        }
        public Guid BasketId { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
    }
}
