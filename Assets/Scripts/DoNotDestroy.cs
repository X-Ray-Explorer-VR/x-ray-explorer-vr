using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
