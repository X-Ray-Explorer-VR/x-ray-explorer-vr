using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("References")]
    public GameObject loadingScreen;
    private FindMainCamera findMainCamera;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        DontDestroyOnLoad(this);
        // Get required components
        findMainCamera = GetComponent<FindMainCamera>();
        canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
    }

    public void LoadScene(string name)
    {
        StartCoroutine(StartLoad(name));
    }

    IEnumerator StartLoad(string name)
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        while (!operation.isDone)
        {
            yield return null;
        }

        // Find the main camera in scene
        findMainCamera.Find();

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0.0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetValue;
    }
}
