using Ardalis.Specification.EntityFrameworkCore;
using SharedKernel.Interfaces;

namespace CheckoutManagement.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
