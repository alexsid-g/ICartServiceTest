# ICartServiceTest

## Discount Store
Implement the code for a discount store checkout that calculates the total price of all items. Goods are priced individually, however there are special offers from time to time when multiple items are bought. For example: “Big mug is 1€ or 2 for 1.5€”.
|SKU         |	Price	|Discount   |
|------------|----------|-----------|
|Vase        |	1.2€	|           |
|------------|----------|-----------|
|Big mug     |	1€	    |2 for 1.5€ |
|Napkins pack|	0.45€	|3 for 0.90€|

The checkout accepts the items in any order, so that if we add a big mug, a vase and then another big mug, we’ll recognise two mugs apply the discount of 2 for 1.5€.  You should be able to add multiple items and then call a totalling method which will return you a price for all items you have added. Removing method should work in the same logic manner.
Here’s a suggested interface (although do feel free to change this or use your own)
```
public interface ICartService
{
    void Add(Item item);
    void Remove(Item item);
    double GetTotal();
}
```
This problem is not a difficult problem to solve and we would expect any level of developer to be able to implement a solution, however we are not just looking for a solution; you MUST demonstrate the following:
1. A clean and simple coding style demonstrating SOLID principles
2. A test-first mentality approach with a unit-test coverage of important methods
Feel free to use your favourite Inversion of Control library if so wish, although this is not required.  
Please commit your solution to GitHub and link us to the repo in your response
