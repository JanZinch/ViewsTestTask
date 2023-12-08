using System;
namespace Purchases
{
    public class Purchase
    {
        public ResourcePrice Price { get; private set; }
        public Action PurchaseAction { get; private set; }
        
        public Purchase(ResourcePrice price, Action purchaseAction)
        {
            Price = price;
            PurchaseAction = purchaseAction;
        }
        
    }
}