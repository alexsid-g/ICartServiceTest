using FluentAssertions;
using ICartServiceTest.CartServices;
using ICartServiceTest.DiscountServices;
using ICartServiceTest.DiscountServices.Discounts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICartServiceTest.Tests
{
    [TestClass]
    public class DiscountServiceFacts
    {
        private const string SKU1 = "sku 1";
        private const string SKU2 = "sku 2";

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
            var discountService = _provider.GetRequiredService<IDiscountService>();

            discountService.Should().NotBeNull();
        }

        [TestMethod]
        public void When_No_Discounts_CanApply_Should_Be_False()
        {
            var items = new[] { new CartItem() { Sku = SKU1 } };
            var discountService = _provider.GetRequiredService<IDiscountService>();

            discountService.CanApplyDiscount(items).Should().BeFalse();
        }

        [TestMethod]
        public void Can_Apply_Discount_To_Items()
        {
            var items1 = new[] { new CartItem() { Sku = SKU1 } };
            var items2 = new[] { new CartItem() { Sku = SKU2 } };
            var discountService = _provider.GetRequiredService<IDiscountService>();
            discountService.Add(new MultipleItemsFixedPriceDiscount(SKU1, 1, 1.0));

            discountService.CanApplyDiscount(items1).Should().BeTrue();
            discountService.CanApplyDiscount(items2).Should().BeFalse();
        }

        [TestMethod]
        public void When_Clear_Discount_CanApply_Should_Be_False()
        {
            var items1 = new[] { new CartItem() { Sku = SKU1 } };
            var discountService = _provider.GetRequiredService<IDiscountService>();
            discountService.Add(new MultipleItemsFixedPriceDiscount(SKU1, 1, 1.0));
            discountService.Clear();

            discountService.CanApplyDiscount(items1).Should().BeFalse();
        }
    }
}
