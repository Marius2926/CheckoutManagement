using CheckoutManagement.Api.Dtos;
using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.Exceptions;
using SharedKernel.Interfaces;

namespace CheckoutManagement.Api.Endpoints
{
    public static class PutBasket
    {
        public static async Task<IResult> Handle(Guid basketId, ArticleLineDto articleLineDto, IRepository<Basket> basketRepository)
        {
            var basket = await basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                throw new BasketNotFoundException();
            }
            if (basket.Status.Closed == true)
            {
                throw new BasketClosedException();
            }
            var articleLine = new ArticleLine(basketId, articleLineDto.Item, articleLineDto.Price);
            basket.AddArticleLine(articleLine);
            await basketRepository.SaveChangesAsync();
            return Results.Ok();
        }
    }
}
