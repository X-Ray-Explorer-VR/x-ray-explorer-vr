using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField]
    private GameObject audioManagerPrefab;
    [Header("Scene Manager")]
    [SerializeField]
    private GameObject sceneManagerPrefab;

    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Audio Manager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0, 0 ,0), Quaternion.identity);
        }

        if (!GameObject.FindGameObjectWithTag("Scene Manager"))
        {
            Instantiate(sceneManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
