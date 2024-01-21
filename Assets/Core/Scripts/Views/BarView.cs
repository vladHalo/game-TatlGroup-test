using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views
{
    public class BarView : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetValue(float value) => _bar.fillAmount = value;

        public float GetValue() => _bar.fillAmount;
    }
}