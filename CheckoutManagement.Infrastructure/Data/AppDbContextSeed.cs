using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.SyncedAggregates;
using CkeckoutManagement.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CheckoutManagement.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        private readonly Guid _basketId = Guid.Parse("9245fe4a-d402-451c-b9ed-9c1a04247482");
        private readonly int _customerId = 1;

        private readonly AppDbContext _context;
        private readonly ILogger<AppDbContextSeed> _logger;

        public AppDbContextSeed(AppDbContext context, ILogger<AppDbContextSeed> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task SeedAsync(int retry = 0)
        {
            _logger.LogInformation("Seeding data");

            try
            {
                _context.Database.EnsureCreated();

                if (!await _context.Baskets.AnyAsync())
                {
                    await _context.Baskets.AddAsync(CreateBasket());
                    await _context.SaveChangesAsync();
                }

                if (!await _context.ArticleLines.AnyAsync())
                {
                    var articleLines = await CreateArticleLines();
                    var basket = await _context.Baskets.FindAsync(_basketId);
                    foreach (var articleLine in articleLines)
                    {
                        basket.AddArticleLine(articleLine);
                    }
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Customers.AnyAsync())
                {
                    await _context.Customers.AddAsync(CreateCustomer());
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retry < 3)
                {
                    retry++;
                    _logger.LogError(ex.Message);
                    Thread.Sleep(5000);
                    await SeedAsync(retry);
                }
                throw;
            }
            _logger.LogInformation("FINISHED - Seeding data");
        }

        private Basket CreateBasket()
        {
            return new Basket(_basketId, _customerId, new BasketStatus(false, false), new BasketValue(504, 504, false));
        }

        private async Task<List<ArticleLine>> CreateArticleLines()
        {
            string fileName = "articleLines.json";
            if (!File.Exists(fileName))
            {
                using Stream writer = new FileStream(fileName, FileMode.OpenOrCreate);
                await JsonSerializer.SerializeAsync(writer, GetDefaultArticleLines());
            }

            using Stream reader = new FileStream(fileName, FileMode.Open);
            var articleLines = await JsonSerializer.DeserializeAsync<List<ArticleLine>>(reader);

            return articleLines.ToList();
        }

        private List<ArticleLine> GetDefaultArticleLines()
        {
            List<ArticleLine> articleLines = new List<ArticleLine>();
            Random random = new Random();
            for (int i = 1; i <= 10; i++)
            {
                articleLines.Add(new(_basketId, $"Article {i}", random.Next() % 100));
            }
            return articleLines;
        }

        private Customer CreateCustomer()
        {
            return new Customer("Customer");
        }
    }
}
