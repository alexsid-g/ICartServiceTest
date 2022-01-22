using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ICartServiceTest.CartServices;
using ICartServiceTest.DiscountServices;
using ICartServiceTest.DiscountServices.Discounts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICartServiceTest.Tests
{
    [TestClass]
    public class CartServiceFacts
    {
        private const string SKU1 = "sku 1";
        private const string SKU2 = "sku 2";
        private const string SKU3 = "sku 3";

        private static ServiceProvider _provider;

        [TestInitialize]
        public void TestInitialize()
        {
            var services = new ServiceCollection();
            _provider = services
                .InitializeCartServiceTest()
                .BuildServiceProvider();
        }

        [TestMethod]
        public void Can_Create_Service()
        {
            var cartService = _provider.GetRequiredService<ICartService>();

            cartService.Should().NotBeNull();
        }

        [TestMethod]
        public void When_No_Discounts_Should_Calc_Total_Sum()
        {
            var discountService = _provider.GetRequiredService<IDiscountService>();
            discountService.Clear();

            var items = GetCartItems();
            var cartService = _provider.GetRequiredService<ICartService>();
            foreach (var itm in items)
            {
                cartService.Add(itm);
            }

            cartService.GetTotal().Should().Be(items.Sum(x => x.Price));
        }

        [TestMethod]
        public void When_No_Proper_Discounts_Should_Calc_Total_Sum()
        {
            var discountService = _provider.GetRequiredService<IDiscountService>();
            discountService.Add(new MultipleItemsFixedPriceDiscount(SKU3, 1, 1.0));

            var items = GetCartItems();
            var cartService = _provider.GetRequiredService<ICartService>();
            foreach (var itm in items)
            {
                cartService.Add(itm);
            }

            cartService.GetTotal().Should().Be(items.Sum(x => x.Price));
        }

        [TestMethod]
        public void When_Proper_Discounts_Should_Calc_Total_Sum()
        {
            var discountService = _provider.GetRequiredService<IDiscountService>();
            discountService.Add(new MultipleItemsFixedPriceDiscount(SKU2, 1, 1.0));

            var items = GetCartItems();
            var cartService = _provider.GetRequiredService<ICartService>();
            foreach (var itm in items)
            {
                cartService.Add(itm);
            }

            cartService.GetTotal().Should().NotBe(items.Sum(x => x.Price));
            cartService.GetTotal().Should().Be(3);
        }

        private IEnumerable<CartItem> GetCartItems()
        {
            return new[]
            {
                new CartItem() { Sku = SKU1, Price = 1 },
                new CartItem() { Sku = SKU1, Price = 1 },
                new CartItem() { Sku = SKU2, Price = 2 },
            };
        }

    }
}
