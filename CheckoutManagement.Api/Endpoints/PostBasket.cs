using CheckoutManagement.Api.Dtos;
using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.BasketAggregate.Specifications;
using CkeckoutManagement.Core.Exceptions;
using CkeckoutManagement.Core.SyncedAggregates;
using CkeckoutManagement.Core.SyncedAggregates.Specifications;
using CkeckoutManagement.Core.ValueObjects;
using SharedKernel.Interfaces;

namespace CheckoutManagement.Api.Endpoints
{
    public static class PostBasket
    {
        public static async Task<IResult> Handle(PostBasketDto postBasketDto, IRepository<Basket> basketRepository, IRepository<Customer> customerRepository)
        {
            var customerSpec = new CustomerByNameSpec(postBasketDto.Customer);
            var existingCustomer = await customerRepository.GetBySpecAsync(customerSpec);

            var basketSpec = new BasketOpenByCustomerIdSpec(existingCustomer.Id);
            var openCustomerBasket = await basketRepository.GetBySpecAsync(basketSpec);

            if (openCustomerBasket != null)
            {
                throw new BasketAlreadyInProgressException();
            }

            if (existingCustomer == null)
            {
                await customerRepository.AddAsync(new Customer(postBasketDto.Customer));
                await customerRepository.SaveChangesAsync();
                existingCustomer = await customerRepository.GetBySpecAsync(customerSpec);
            }

            Basket basket = new Basket(Guid.NewGuid(), existingCustomer.Id, new BasketStatus(false, false), new BasketValue(0, 0, postBasketDto.PaysVAT));
            await basketRepository.AddAsync(basket);
            await basketRepository.SaveChangesAsync();
            return Results.Ok(basket.Id);
        }
    }
}
