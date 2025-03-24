using UnityEngine;

public class CallSceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();

        if (sceneLoader)
        {
            sceneLoader.LoadScene(sceneName);
        }
    }
}
