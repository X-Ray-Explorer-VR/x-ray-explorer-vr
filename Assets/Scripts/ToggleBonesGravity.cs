using UnityEngine;
using UnityEngine.UI;

public class ToggleBonesGravity : MonoBehaviour
{
    [Header("References")]
    public GameObject skeleton;
    private Rigidbody[] bonesRb;

    private void Start()
    {
        bonesRb = skeleton.gameObject.GetComponentsInChildren<Rigidbody>();
    }

    public void ToggleGravity(Toggle change)
    {
        foreach (Rigidbody item in bonesRb)
        {
            item.isKinematic = !change.isOn;
        }
    }
}
