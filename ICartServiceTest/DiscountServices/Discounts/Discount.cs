using System;
using System.Collections.Generic;
using ICartServiceTest.CartServices;

namespace ICartServiceTest.DiscountServices.Discounts
{
    public abstract class Discount
    {
        private string _sku;

        protected Discount(string sku)
        {
            _sku = sku;
        }

        public bool CanApplyTo(CartItem x)
        {
            return _sku.Equals(x.Sku);
        }

        public abstract double GetTotal(IEnumerable<CartItem> items);
    }
}