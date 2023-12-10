using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bonuses.Models
{
    [CreateAssetMenu(fileName = "daily_bonuses_container", menuName = "Containers/DailyBonusesContainer", order = 0)]
    public class DailyBonusesContainer : ScriptableObject
    {
        [SerializeField] private List<DailyBonus> _dailyBonuses;

        public int AllBonusesCount => _dailyBonuses.Count;
        
        public DailyBonus GetBonusByIndex(int bonusIndex)
        {
            if (bonusIndex < 0 || bonusIndex >= _dailyBonuses.Count)
            {
                Debug.LogException(
                    new ArgumentOutOfRangeException(nameof(bonusIndex), bonusIndex, "is out of bonuses array"));
                
                return null;
            }
            else
            {
                return _dailyBonuses[bonusIndex];
            }
        }

    }
}