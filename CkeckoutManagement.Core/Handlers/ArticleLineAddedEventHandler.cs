using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.BasketAggregate.Specifications;
using CkeckoutManagement.Core.Events;
using MediatR;
using SharedKernel.Interfaces;

namespace CkeckoutManagement.Core.Handlers
{
    public class ArticleLineAddedEventHandler : INotificationHandler<ArticleLineAddedEvent>
    {
        private readonly IRepository<Basket> _basketRepository;

        public ArticleLineAddedEventHandler(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(ArticleLineAddedEvent articleLineAddedEvent,
          CancellationToken cancellationToken)
        {

            var basket = await _basketRepository.GetByIdAsync(articleLineAddedEvent.BasketId);

            basket.Value.AddedNewItem(articleLineAddedEvent.ArticleLineAdded.Price);
            await _basketRepository.UpdateAsync(basket);
        }
    }
}
