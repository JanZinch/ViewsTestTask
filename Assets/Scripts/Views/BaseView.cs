using UnityEngine;

namespace Views
{
    public class BaseView : MonoBehaviour
    {
        public void Hide()
        {
            Destroy(gameObject);
        }
    }
}