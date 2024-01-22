using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Core.Scripts.Views
{
    public class CanvasChanger : MonoBehaviour
    {
        [SerializeField] private float _delay = .2f, _durationFade = .3f;
        private WaitForSeconds _waitForSeconds;

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_delay);
        }

        public void ShowCanvas(CanvasGroup canvasGroup)
        {
            StartCoroutine(ProcessCanvasGroup(canvasGroup, 0f, 1f, true));
        }

        public void HideCanvas(CanvasGroup canvasGroup)
        {
            StartCoroutine(ProcessCanvasGroup(canvasGroup, 1f, 0f, false));
        }

        private IEnumerator ProcessCanvasGroup(CanvasGroup canvasGroup, float initAlpha, float alpha, bool enable)
        {
            canvasGroup.alpha = initAlpha;
            if (enable) canvasGroup.gameObject.SetActive(true);
            yield return _waitForSeconds;
            canvasGroup.DOFade(alpha, _durationFade);
            yield return _waitForSeconds;
            canvasGroup.gameObject.SetActive(enable);
        }
    }
}