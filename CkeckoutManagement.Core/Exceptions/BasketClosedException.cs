namespace CkeckoutManagement.Core.Exceptions
{
    public class BasketClosedException : Exception
    {
        public BasketClosedException() : base("You can't add items to a closed basket.")
        {
        }
        public BasketClosedException(string message) : base(message)
        {
        }
    }
}
