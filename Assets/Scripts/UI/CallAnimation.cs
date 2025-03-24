using UnityEngine;

public class CallAnimation : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private string parameterName;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBoolean(bool value)
    {
        _animator.SetBool(parameterName, value);
    }

    public void CallTrigger(string parameter)
    {
        _animator.SetTrigger(parameter);
    }
}
