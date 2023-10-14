using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.HUD
{
    public class BuffDisplayController : MonoBehaviour
    {
        [SerializeField] private Image _buffIco;
        [SerializeField] private UnityEvent<float> _buffTimeChangeEvent;

        public void Setup(Sprite ico)
        {
            _buffIco.sprite = ico;
        }

        public void UpdateLeftTime(float value)
        {
            _buffTimeChangeEvent.Invoke(value);
        }
    }
}