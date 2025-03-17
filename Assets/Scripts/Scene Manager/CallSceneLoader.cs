using UnityEngine;

public class CallSceneLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();

        if (sceneLoader)
        {
            sceneLoader.LoadScene(name);
        }
    }
}
