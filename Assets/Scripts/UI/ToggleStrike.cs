using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleStrike : MonoBehaviour
    {
        [SerializeField] private GameObject _strikeMark;
        private Toggle _toggle;
        
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            OnToggleValueChanged(_toggle.isOn);
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool value)
        {
            _strikeMark.SetActive(!value);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}