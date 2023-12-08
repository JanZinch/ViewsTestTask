using Factories;
using Progress;
using Purchases;
using UnityEngine;
using Views;

namespace Management
{
    public class MenuSceneRoot : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private ViewsFactory _viewsFactory;
        
        private void Awake()
        {
            UnityServicesUtility.Initialize();
            
            _menuView.InjectDependencies(_viewsFactory);
        }
    }
}
