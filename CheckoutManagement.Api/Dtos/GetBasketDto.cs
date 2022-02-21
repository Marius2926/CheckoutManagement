using CkeckoutManagement.Core.BasketAggregate;

namespace CheckoutManagement.Api.Dtos
{
    public class GetBasketDto
    {
        public Guid Id { get; set; }
        public IEnumerable<ArticleLineDto> Items { get; set; }
        public double TotalNet { get; set; }
        public double TotalGross { get; set; }
        public bool PaysVAT { get; set; }
        public string Customer { get; set; }
    }
}
