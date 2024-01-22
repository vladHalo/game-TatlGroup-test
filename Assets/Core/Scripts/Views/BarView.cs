using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views
{
    public class BarView : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private bool _isStartFill;

        [ShowIf("_isStartFill")] [Range(0, 100)] [SerializeField]
        private int _barFillMax;

        [ShowIf("_isStartFill")] [SerializeField]
        private Text _text;

        private int _barFill;
        
        private void Start()
        {
            if (_isStartFill) StartCoroutine(RunLoad());
        }

        private IEnumerator RunLoad()
        {
            while (_barFill < _barFillMax)
            {
                _bar.fillAmount += .01f;
                _barFill++;
                _text.text = $"{_barFill}%";
                yield return new WaitForSeconds(.001f);
            }
        }

        public void SetValue(float value) => _bar.fillAmount = value;
    }
}