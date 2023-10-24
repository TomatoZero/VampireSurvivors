using DG.Tweening;
using UnityEngine;

namespace UI.Appear
{
    public class FadeAppear : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime;

        public void OpenEventHandler()
        {
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1, _fadeTime);
        }

        public void CloseEventHandler()
        {
            gameObject.SetActive(false);
            _canvasGroup.DOFade(0, _fadeTime);
        }
    }
}