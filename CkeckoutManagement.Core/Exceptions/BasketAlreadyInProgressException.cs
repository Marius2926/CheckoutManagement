namespace CkeckoutManagement.Core.Exceptions
{
    public class BasketAlreadyInProgressException : Exception
    {
        public BasketAlreadyInProgressException() : base("Customer has a cart already in progress.")
        {
        }
        public BasketAlreadyInProgressException(string message) : base(message)
        {
        }
    }
}
