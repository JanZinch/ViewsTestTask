using System;
using InAppResources;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class DailyBonus
    {
        [field:SerializeField] public ResourceType ResourceType { get; private set; }
        [field:SerializeField] public double ResourceAmount { get; private set; }

        public DailyBonus(ResourceType resourceType, double resourceAmount)
        {
            ResourceType = resourceType;
            ResourceAmount = resourceAmount;
        }
    }
}