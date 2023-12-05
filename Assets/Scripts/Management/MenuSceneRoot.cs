using Factories;
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
            _menuView.InjectDependencies(_viewsFactory);
        }
    }
}
