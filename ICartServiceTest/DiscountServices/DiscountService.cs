using System;
using System.Collections.Generic;
using System.Linq;
using ICartServiceTest.CartServices;
using ICartServiceTest.DiscountServices.Discounts;

namespace ICartServiceTest.DiscountServices
{
    internal class DiscountService : IDiscountService
    {
        private readonly List<Discount> _discounts = new List<Discount>();

        public void Add(Discount discount)
        {
            _discounts.Add(discount);
        }

        public void Clear()
        {
            _discounts.Clear();
        }

        public bool CanApplyDiscount(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Any(x => _discounts.Any(d => d.CanApplyTo(x)));
        }

        public double GetTotal(IEnumerable<CartItem> cartItems)
        {
            var result = 0.0;
            var itemsByDiscount = cartItems
                .GroupBy(x => _discounts.FirstOrDefault(d => d.CanApplyTo(x)))
                .ToList();

            foreach (var item in itemsByDiscount)
            {
                var discount = item.Key;
                result += discount != null
                    ? discount.GetTotal(item)
                    : item.Sum(x => x.Price);
            }

            return result;
        }
    }
}
