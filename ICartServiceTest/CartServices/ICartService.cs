using System;
using System.Collections.Generic;
using System.Text;

namespace ICartServiceTest.CartServices
{
    public interface ICartService
    {
        void Add(CartItem item);

        void Remove(CartItem item);

        double GetTotal();
    }
}
