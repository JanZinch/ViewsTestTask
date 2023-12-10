using System;

namespace Purchases.InAppPurchases
{
    public struct PendingPair
    {
        public InAppPurchase Purchase { get; private set; }
        public Action<bool> OnCompleteCallback { get; private set; }

        public PendingPair(InAppPurchase purchase, Action<bool> onCompleteCallback)
        {
            Purchase = purchase;
            OnCompleteCallback = onCompleteCallback;
        }

        public bool ProcessIfPossible()
        {
            if (Purchase != null)
            {
                Purchase.ProcessPurchase?.Invoke();
                OnCompleteCallback?.Invoke(true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}