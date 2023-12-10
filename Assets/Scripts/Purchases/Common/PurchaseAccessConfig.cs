using System;
using System.Collections.Generic;
using UnityEngine;

namespace Purchases.Common
{
    [CreateAssetMenu(fileName = "purchase_access_config", menuName = "Configs/PurchaseAccessConfig", order = 0)]
    public class PurchaseAccessConfig : ScriptableObject
    {
        [SerializeField] private List<Access> _accesses;
        
        [Serializable]
        public struct Access
        {
            [field:SerializeField] public PurchaseType PurchaseType { get; private set; }
            [field:SerializeField] public int RequiredLevelIndex { get; private set; }
        }

        public int GetRequiredLevelIndex(PurchaseType purchaseType)
        {
            return _accesses.Find(access => access.PurchaseType == purchaseType).RequiredLevelIndex;
        }
    }
}