using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
