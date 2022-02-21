using CkeckoutManagement.Core.BasketAggregate;
using SharedKernel;

namespace CkeckoutManagement.Core.Events
{
    public class ArticleLineAddedEvent : BaseDomainEvent
    {
        public ArticleLineAddedEvent(Guid basketId, ArticleLine articleLineAdded)
        {
            BasketId = basketId;
            ArticleLineAdded = articleLineAdded;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public ArticleLine ArticleLineAdded { get; private set; }
        public Guid BasketId { get; private set; }
    }
}
