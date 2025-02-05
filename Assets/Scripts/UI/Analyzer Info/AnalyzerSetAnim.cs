using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzerSetAnim : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetEntryState(bool visible)
    {
        animator.SetBool("show", visible);
    }
}
