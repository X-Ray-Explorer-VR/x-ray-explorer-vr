using UnityEngine;

public class CallTriggerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CallTrigger(string name)
    {
        animator.SetTrigger(name);
    }
}
