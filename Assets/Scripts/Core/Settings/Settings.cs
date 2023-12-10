using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Settings
{
    public class Settings
    {
        private const string IsMusicOnKey = "is_music_on";
        private const string IsSoundsOnKey = "is_sounds_on";

        private const string MusicVolumeParam = "music_volume";
        private const string SoundsVolumeParam = "sounds_volume";

        private const float OnDecibels = 0.0f;
        private const float OffDecibels = -80.0f;
        
        private readonly AudioMixer _audioMixer;

        public Settings(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
            
            _audioMixer.SetFloat(MusicVolumeParam, BooleanToDecibels(IsMusicOn));
            _audioMixer.SetFloat(SoundsVolumeParam, BooleanToDecibels(IsSoundsOn));
        }
        
        public bool IsMusicOn
        {
            get => Convert.ToBoolean(PlayerPrefs.GetInt(IsMusicOnKey, Convert.ToInt32(true)));

            set
            {
                _audioMixer.SetFloat(MusicVolumeParam, BooleanToDecibels(value));
                PlayerPrefs.SetInt(IsMusicOnKey, Convert.ToInt32(value));
                PlayerPrefs.Save();
            }
        }

        public bool IsSoundsOn
        {
            get => Convert.ToBoolean(PlayerPrefs.GetInt(IsSoundsOnKey, Convert.ToInt32(true)));

            set
            {
                _audioMixer.SetFloat(SoundsVolumeParam, BooleanToDecibels(value));
                PlayerPrefs.SetInt(IsSoundsOnKey, Convert.ToInt32(value));
                PlayerPrefs.Save();
            }
        }

        private static float BooleanToDecibels(bool value)
        {
            return value ? OnDecibels : OffDecibels;
        }

    }
}