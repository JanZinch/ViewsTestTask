using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace Purchases
{
    public static class UnityServicesUtility
    {
        private const string EnvironmentName = "dev";

        public static async void Initialize()
        {
            try {
                
                InitializationOptions options = new InitializationOptions().SetEnvironmentName(EnvironmentName);
                await UnityServices.InitializeAsync(options);
            }
            catch (Exception exception) {
                
                Debug.LogException(exception);
            }
        }
        
    }
}