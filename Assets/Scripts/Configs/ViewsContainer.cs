using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Views;

namespace Configs
{
    [CreateAssetMenu(fileName = "views_container", menuName = "Containers/ViewsContainer", order = 0)]
    public class ViewsContainer : ScriptableObject
    {
        [SerializeField] private List<BaseView> _viewPrefabs;

        public TView GetViewPrefab<TView>() where TView : BaseView
        {
            foreach (BaseView viewPrefab in _viewPrefabs)
            {
                if (viewPrefab is TView concreteViewPrefab)
                {
                    return concreteViewPrefab;
                }
            }

            return null;
        }
    }
}