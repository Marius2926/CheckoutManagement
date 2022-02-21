using SharedKernel;

namespace CkeckoutManagement.Core.ValueObjects
{
    public class BasketStatus : ValueObject
    {
        public bool Closed { get; private set; }
        public bool Payed { get; private set; }

        public BasketStatus(bool closed, bool payed)
        {
            Closed = closed;
            Payed = payed;
        }

        public void UpdateStatus(bool closed, bool payed)
        {
            this.Closed = closed;
            this.Payed = payed;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Closed;
            yield return Payed;
        }
    }
}
