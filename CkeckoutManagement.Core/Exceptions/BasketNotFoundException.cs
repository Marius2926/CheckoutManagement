namespace CkeckoutManagement.Core.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException() : base("Basket not found.")
        {
        }
        public BasketNotFoundException(string message) : base(message)
        {
        }
    }
}
