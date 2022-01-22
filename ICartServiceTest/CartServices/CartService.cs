using System.Collections.Generic;
using System.Linq;
using ICartServiceTest.DiscountServices;

namespace ICartServiceTest.CartServices
{
    internal class CartService : ICartService
    {
        private readonly List<CartItem> _items = new List<CartItem>();

        private readonly IDiscountService _discounter;

        public CartService(IDiscountService discounter) =>
            _discounter = discounter;

        public void Add(CartItem item)
        {
            _items.Add(item);
        }

        public void Remove(CartItem item)
        {
            _items.Remove(item);
        }

        public double GetTotal()
        {
            if (_discounter.CanApplyDiscount(_items))
            {
                return _discounter.GetTotal(_items);
            }

            return _items.Sum(x => x.Price);
        }
    }
}
