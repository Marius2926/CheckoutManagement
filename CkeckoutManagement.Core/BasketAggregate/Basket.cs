using Ardalis.GuardClauses;
using CkeckoutManagement.Core.Events;
using CkeckoutManagement.Core.ValueObjects;
using SharedKernel;
using SharedKernel.Interfaces;

namespace CkeckoutManagement.Core.BasketAggregate
{
    public class Basket : BaseEntity<Guid>, IAggregateRoot
    {
        public Basket(Guid id, int customerId, BasketStatus basketStatus, BasketValue basketValue)
        {
            Id = Guard.Against.Default(id, nameof(id));
            CustomerId = Guard.Against.NegativeOrZero(customerId, nameof(customerId));
            Status = basketStatus;
            Value = basketValue;
        }

        public Basket(Guid id, int customerId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            CustomerId = Guard.Against.NegativeOrZero(customerId, nameof(customerId));
        }

        public int CustomerId { get; private set; }
        private readonly List<ArticleLine> _articleLines = new List<ArticleLine>();
        public IEnumerable<ArticleLine> ArticleLines => _articleLines.AsReadOnly();
        public BasketStatus Status { get; private set; }
        public BasketValue Value { get; private set; }

        public ArticleLine AddArticleLine(ArticleLine article)
        {
            _articleLines.Add(article);

            var articleLineAddedEvent = new ArticleLineAddedEvent(Id, article);
            Events.Add(articleLineAddedEvent);

            return article;
        }
    }
}
