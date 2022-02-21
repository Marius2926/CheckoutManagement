using CheckoutManagement.Api.Dtos;
using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.BasketAggregate.Specifications;
using CkeckoutManagement.Core.Exceptions;
using SharedKernel.Interfaces;

namespace CheckoutManagement.Api.Endpoints
{
    public static class PatchBasket
    {
        public static async Task<IResult> Handle(Guid basketId, PatchBasketDto patchBasketDto, IRepository<Basket> basketRepository)
        {
            var basket = await basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                throw new BasketNotFoundException();
            }
            if (basket.Status.Closed == true && patchBasketDto.Close == false)
            {
                //check if the customer has already an basket open
                var spec = new BasketOpenByCustomerIdSpec(basket.CustomerId);
                var openBaskets = await basketRepository.ListAsync(spec);
                if (openBaskets.Count > 0)
                {
                    throw new BasketAlreadyInProgressException($"Before opening this basket, close the following baskets: {openBaskets.Select(ob => ob.Id.ToString()).Aggregate((l, r) => l + ", " + r)}.");
                }
            }
            basket.Status.UpdateStatus(patchBasketDto.Close, patchBasketDto.Payed);
            await basketRepository.SaveChangesAsync();
            return Results.Ok();
        }
    }
}
