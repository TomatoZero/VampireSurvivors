using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PanelAppearController : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _fadeTime;
        
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = _rectTransform.transform.localPosition;
        }

        public void OpenEventHandler()
        {
            _rectTransform.DOAnchorPos(Vector3.zero, _fadeTime).SetEase(_curve);
            _canvasGroup.DOFade(1, _fadeTime);
        }

        public void CloseEventHandler()
        {
            _rectTransform.DOAnchorPos(-_rectTransform.transform.localPosition * 2, _fadeTime).SetEase(Ease.InOutQuint);
            _canvasGroup.DOFade(0, _fadeTime);
        }
    }
}