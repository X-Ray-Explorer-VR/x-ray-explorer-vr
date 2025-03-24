using UnityEngine;

public class FindMainCamera : MonoBehaviour
{

    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Find()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera)
        {
            _canvas.worldCamera = mainCamera;
        }
    }
}
