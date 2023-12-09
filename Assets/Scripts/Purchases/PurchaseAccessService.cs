using System.Collections.Generic;

namespace Purchases
{
    public class PurchaseAccessService
    {
        private Dictionary<PurchaseType, int> _requiredLevels = new Dictionary<PurchaseType, int>()
        {
            { PurchaseType.Character2, 10 },
            { PurchaseType.Location3, 10 },
        };
        
        //public int GetRequiredLevel
        
    }
}