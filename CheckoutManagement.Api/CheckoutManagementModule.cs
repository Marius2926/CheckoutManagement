using CheckoutManagement.Api.Dtos;
using CheckoutManagement.Api.Endpoints;
using CheckoutManagement.Infrastructure.Data;
using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.SyncedAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using System.Reflection;

namespace CheckoutManagement.Api
{
    public static class CheckoutManagementModule
    {
        public static IServiceCollection RegisterCheckoutModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<AppDbContextSeed>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            var assemblies = new Assembly[]
            {
                    typeof(Program).Assembly,
                    typeof(AppDbContext).Assembly,
                    typeof(Basket).Assembly
            };
            services.AddMediatR(assemblies);
            return services;
        }

        public static IEndpointRouteBuilder MapCheckoutEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/baskets/{id}", (Guid id, IRepository<Basket> basketRepository, IRepository<Customer> customerRepository) =>
            {
                return GetBasket.Handle(id, basketRepository, customerRepository);
            });
            endpoints.MapPost("/baskets", (PostBasketDto postBasketDto, IRepository<Basket> basketRepository, IRepository<Customer> customerRepository) =>
            {
                return PostBasket.Handle(postBasketDto, basketRepository, customerRepository);
            });
            endpoints.MapPut("/baskets/{id}/article-line", (Guid id, ArticleLineDto articleLineDto, IRepository<Basket> basketRepository) =>
            {
                return PutBasket.Handle(id, articleLineDto, basketRepository);
            });
            endpoints.MapMethods("/baskets/{id}", new[] { "patch" }, (Guid id, PatchBasketDto patchBasketDto, IRepository<Basket> basketRepository) =>
            {
                return PatchBasket.Handle(id, patchBasketDto, basketRepository);
            });
            return endpoints;
        }
    }
}
