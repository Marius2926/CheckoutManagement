using Ardalis.Specification;

namespace CkeckoutManagement.Core.BasketAggregate.Specifications
{
    public class BasketByIdWithArticleLinesSpec : Specification<Basket>, ISingleResultSpecification
    {
        public BasketByIdWithArticleLinesSpec(Guid basketId)
        {
            Query
              .Where(basket => basket.Id == basketId)
              .Include(basket => basket.ArticleLines);
        }
    }
}
