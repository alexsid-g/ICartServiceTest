using FluentAssertions;
using ICartServiceTest.CartServices;
using ICartServiceTest.DiscountServices;
using ICartServiceTest.DiscountServices.Discounts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ICartServiceTest.Tests
{
    [TestClass]
    public class DiscountFacts
    {
        private const string SKU1 = "sku 1";
        private const string SKU2 = "sku 2";

        [TestMethod]
        public void Can_Create_Discount()
        {
            var discount = new MultipleItemsFixedPriceDiscount(SKU1, 1, 1.0);
            discount.Should().NotBeNull();
        }

        [TestMethod]
        public void Can_Apply_Discount_To_Item()
        {
            var cartItem1 = new CartItem() { Sku = SKU1 };
            var cartItem2 = new CartItem() { Sku = SKU2 };
            var discount = new MultipleItemsFixedPriceDiscount(SKU1, 1, 1.0);

            discount.CanApplyTo(cartItem1).Should().BeTrue();
            discount.CanApplyTo(cartItem2).Should().BeFalse();
        }

        [TestMethod]
        public void Should_Calculate_Discount_To_Item()
        {
            var cartItem1 = new CartItem() { Sku = SKU1, Price = 2 };
            var cartItem2 = new CartItem() { Sku = SKU2, Price = 2 };
            var discount = new MultipleItemsFixedPriceDiscount(SKU1, 1, 1.0);

            discount.GetTotal(new[] { cartItem1 }).Should().Be(1);
            discount.GetTotal(new[] { cartItem2 }).Should().Be(2);
            discount.GetTotal(new[] { cartItem1, cartItem2 }).Should().Be(3);
        }

        [DataTestMethod]
        [DataRow(SKU1, 3, 1, 3, 1, 1)]
        [DataRow(SKU2, 7, 5, 4, 15, 30)]
        public void Should_Calculate_Discount_To_Item(string sku, int itemsCount, double itemPrice, int discountCount, double discountPrice, double result)
        {
            var items = new List<CartItem>();
            for (var i = 0; i < itemsCount; i++)
            {
                items.Add(new CartItem() { Sku = sku, Price = itemPrice });
            }
            
            var discount = new MultipleItemsFixedPriceDiscount(SKU1, discountCount, discountPrice);
            discount.CanApplyTo(items[0]).Should().BeTrue();
            discount.GetTotal(items).Should().Be(result);
        }
    }
}
