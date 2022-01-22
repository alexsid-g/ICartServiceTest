using System.Collections.Generic;
using ICartServiceTest.CartServices;
using ICartServiceTest.DiscountServices.Discounts;

namespace ICartServiceTest.DiscountServices
{
    public interface IDiscountService
    {
        public void Add(Discount discount);

        public void Clear();

        public bool CanApplyDiscount(IEnumerable<CartItem> cartItems);

        public double GetTotal(IEnumerable<CartItem> cartItems);
    }
}
