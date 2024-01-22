using Core.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

public class LevelModel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text text;

    public void Init(int indexLevel, CanvasChanger canvasChanger,CanvasGroup hideCanvas,CanvasGroup showCanvas)
    {
        text.text = $"{indexLevel}";
        button.onClick.AddListener(()=>canvasChanger.HideCanvas(hideCanvas));
        button.onClick.AddListener(()=>canvasChanger.ShowCanvas(showCanvas));
    }
}
