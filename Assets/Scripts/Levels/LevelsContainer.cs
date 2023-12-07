using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "levels_container", menuName = "Containers/LevelsContainer", order = 0)]
    public class LevelsContainer : ScriptableObject
    {
        [SerializeField] private List<Level> _dailyBonuses;

        public int AllBonusesCount => _dailyBonuses.Count;
        
        public Level GetBonusByIndex(int bonusIndex)
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