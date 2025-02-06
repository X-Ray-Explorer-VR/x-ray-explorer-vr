using UnityEngine;

public class StartHideAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        animator.SetTrigger("hide");
    }
}
