using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class SetDefaultLazyFollowOffset : MonoBehaviour
{
    private LazyFollow follow;

    private void Start()
    {
        follow = GetComponent<LazyFollow>();
        // Update target offset
        follow.targetOffset = transform.localPosition;
    }
}
