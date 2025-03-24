using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("References")]
    public GameObject loadingScreen;
    private FindMainCamera _findMainCamera;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        DontDestroyOnLoad(this);
        // Get required components
        _findMainCamera = GetComponent<FindMainCamera>();
        _canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(StartLoad(sceneName));
    }

    IEnumerator StartLoad(string sceneName)
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }

        // Find the main camera in scene
        _findMainCamera.Find();

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = _canvasGroup.alpha;
        float time = 0.0f;

        while (time < duration)
        {
            _canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = targetValue;
    }
}
