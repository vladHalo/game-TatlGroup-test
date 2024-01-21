using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Core.Scripts.Views
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button[] _restartButtons;

        [Inject] private GameManager _gameManager;

        private void Start()
        {
            _pauseButton.onClick.AddListener(PauseLevel);
            _restartButtons.ForEach(x => x.onClick.AddListener(RestartLevel));
        }

        private void PauseLevel()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(Str.Main);
        }
    }
}