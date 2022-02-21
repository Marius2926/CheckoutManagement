using Ardalis.Specification;

namespace CkeckoutManagement.Core.BasketAggregate.Specifications
{

    public class BasketOpenByCustomerIdSpec : Specification<Basket>, ISingleResultSpecification
    {
        public BasketOpenByCustomerIdSpec(int customerId)
        {
            Query
              .Where(basket => basket.CustomerId == customerId && basket.Status.Closed == false);
        }
    }
}
