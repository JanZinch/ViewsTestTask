using UnityEngine;

namespace Core.Basics
{
    public class BaseView : MonoBehaviour
    {
        public void Hide()
        {
            Destroy(gameObject);
        }
    }
}