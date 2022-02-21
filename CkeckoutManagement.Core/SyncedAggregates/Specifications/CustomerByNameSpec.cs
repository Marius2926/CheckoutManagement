using Ardalis.Specification;

namespace CkeckoutManagement.Core.SyncedAggregates.Specifications
{
    public class CustomerByNameSpec : Specification<Customer>, ISingleResultSpecification
    {
        public CustomerByNameSpec(string name)
        {
            Query
              .Where(customer => customer.Name == name);
        }
    }
}
