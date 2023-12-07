using System;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public class Level
    {
        [field:SerializeField] public LevelState State { get; private set; }

        public Level(LevelState resourceType)
        {
            State = resourceType;
        }
    }
}