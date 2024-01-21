using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private Slider _progress;

    private void Start()
    {
        _progress.value = 0;
        StartCoroutine(RunLoad());
    }

    private IEnumerator RunLoad()
    {
        while (_progress.value != 1)
        {
            _progress.value += .01f;
            yield return new WaitForSeconds(.001f);
        }

        SceneManager.LoadScene(Str.Main);
    }
}