using Core.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

public class SpawnLevelView : MonoBehaviour
{
    [SerializeField] private int _maxBtn;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private CanvasChanger _canvasChanger;
    [SerializeField] private CanvasGroup _canvasHide;
    [SerializeField] private CanvasGroup _canvasShow;
    
    private int _indexBtn;
    
    private void Start()
    {
        _indexBtn = 1;
        _button.onClick.AddListener(Add);
    }

    public void Add()
    {
        if (_indexBtn < _maxBtn)
        {
            _indexBtn++;
            Instantiate(_prefab,_parent).GetComponent<LevelModel>().Init(_indexBtn,_canvasChanger,_canvasHide,_canvasShow);
        }
    }
}
