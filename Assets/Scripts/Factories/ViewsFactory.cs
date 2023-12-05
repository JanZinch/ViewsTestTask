using Configs;
using UnityEngine;
using Views;

namespace Factories
{
    public class ViewsFactory : MonoBehaviour
    {
        [SerializeField] private RectTransform _viewsParent;
        [SerializeField] private ViewsContainer _viewsContainer;
        
        public TView ShowView<TView>() where TView : BaseView
        {
            TView viewPrefab = _viewsContainer.GetViewPrefab<TView>();

            if (viewPrefab != null)
            {
                return Instantiate<TView>(viewPrefab, _viewsParent, false);
            }
            else
            {
                return null;
            }
        }
        
    }
}