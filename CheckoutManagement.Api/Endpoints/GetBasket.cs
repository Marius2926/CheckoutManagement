using CheckoutManagement.Api.Dtos;
using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.BasketAggregate.Specifications;
using CkeckoutManagement.Core.Exceptions;
using CkeckoutManagement.Core.SyncedAggregates;
using SharedKernel.Interfaces;

namespace CheckoutManagement.Api.Endpoints
{
    public static class GetBasket
    {
        public static async Task<IResult> Handle(Guid basketId, IRepository<Basket> basketRepository, IRepository<Customer> customerRepository)
        {
            var spec = new BasketByIdWithArticleLinesSpec(basketId);
            var basket = await basketRepository.GetBySpecAsync(spec);
            if (basket == null)
            {
                throw new BasketNotFoundException();
            }

            var customer = await customerRepository.GetByIdAsync(basket.CustomerId);
            if (customer == null)
            {
                throw new CustomerNotFoundException();
            }
            var basketDto = new GetBasketDto();
            basketDto.Id = basketId;
            basketDto.TotalNet = basket.Value.TotalNet;
            basketDto.TotalGross = basket.Value.TotalGross;
            basketDto.PaysVAT = basket.Value.PaysVAT;
            basketDto.Items = basket.ArticleLines.Select(al => new ArticleLineDto() { Item = al.Name, Price = al.Price });
            basketDto.Customer = customer.Name;

            return Results.Ok(basketDto);
        }
    }
}
