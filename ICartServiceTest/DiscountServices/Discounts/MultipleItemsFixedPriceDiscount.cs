using System;
using System.Collections.Generic;
using ICartServiceTest.CartServices;

namespace ICartServiceTest.DiscountServices.Discounts
{
    public class MultipleItemsFixedPriceDiscount : Discount
    {
        private int _itemsCount;
        private double _price;

        public MultipleItemsFixedPriceDiscount(string sku, int itemsCount, double price)
            : base(sku)
        {
            _itemsCount = itemsCount;
            _price = price;
        }

        public override double GetTotal(IEnumerable<CartItem> items)
        {
            double result = 0.0;
            int currentItemsCount = 0;

            foreach (var item in items)
            {
                result += item.Price;

                if (CanApplyTo(item))
                {
                    currentItemsCount++;
                    if (currentItemsCount == _itemsCount)
                    {
                        result -= item.Price * currentItemsCount;
                        result += _price;
                        currentItemsCount = 0;
                    }
                }
            }

            return result;
        }
    }
}