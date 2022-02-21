using SharedKernel;
using SharedKernel.Interfaces;

namespace CkeckoutManagement.Core.SyncedAggregates
{
    public class Customer : BaseEntity<int>, IAggregateRoot
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
