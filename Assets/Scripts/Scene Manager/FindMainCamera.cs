using UnityEngine;

public class FindMainCamera : MonoBehaviour
{

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Find()
    {
        Camera camera = Camera.main;

        if (camera)
        {
            canvas.worldCamera = camera;
        }
    }
}
