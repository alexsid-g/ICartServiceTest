using ICartServiceTest.CartServices;
using ICartServiceTest.DiscountServices;
using Microsoft.Extensions.DependencyInjection;

namespace ICartServiceTest
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InitializeCartServiceTest(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDiscountService, DiscountService>();
            serviceCollection.AddTransient<ICartService, CartService>();

            return serviceCollection;
        }
    }
}
