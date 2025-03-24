using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class SetDefaultLazyFollowOffset : MonoBehaviour
{
    private LazyFollow _follow;

    private void Start()
    {
        _follow = GetComponent<LazyFollow>();
        // Update target offset
        _follow.targetOffset = transform.localPosition;
    }
}
