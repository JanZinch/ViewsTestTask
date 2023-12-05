using System;
using UnityEngine;

namespace Models
{
    public class Settings
    {
        private const string IsMusicOnKey = "is_music_on";
        private const string IsSoundsOnKey = "is_sounds_on";
        
        public bool IsMusicOn
        {
            get
            {
                return Convert.ToBoolean(PlayerPrefs.GetInt(IsMusicOnKey, Convert.ToInt32(true)));
            }
            
            set
            {
                PlayerPrefs.SetInt(IsMusicOnKey, Convert.ToInt32(value));
                PlayerPrefs.Save();
            }
        }

        public bool IsSoundsOn
        {
            get
            {
                return Convert.ToBoolean(PlayerPrefs.GetInt(IsSoundsOnKey, Convert.ToInt32(true)));
            }
            
            set
            {
                PlayerPrefs.SetInt(IsSoundsOnKey, Convert.ToInt32(value));
                PlayerPrefs.Save();
            }
        }
    }
}