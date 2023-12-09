using System;
using System.Threading.Tasks;
using Audio;
using Factories;
using Models;
using Progress;
using Purchases;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Audio;
using Views;

namespace Management
{
    public class MenuSceneRoot : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private ViewsFactory _viewsFactory;
        
        [Space]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioManager _audioManager;
        
        private const string EnvironmentName = "dev";

        private static async void InitializeUnityServices(Action onComplete)
        {
            try {
                
                InitializationOptions options = new InitializationOptions().SetEnvironmentName(EnvironmentName);
                await UnityServices.InitializeAsync(options);
                
                onComplete?.Invoke();
            }
            catch (Exception exception) {
                
                Debug.LogException(exception);
            }
        }
        
        private void Awake()
        {
            InitializeUnityServices(() =>
            {
                Settings settings = new Settings(_audioMixer);
                _audioManager.Initialize(settings);
            
            
                _menuView.InjectDependencies(_viewsFactory, settings);
                
                
            });
            
            
        }
    }
}
