using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SocketFilter : MonoBehaviour, IXRSelectFilter, IXRHoverFilter
{
    public bool canProcess => isActiveAndEnabled;
    
    private string _parentName;

    private void Awake()
    {
        _parentName = transform.parent.name.Replace("Base", "").TrimEnd();
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return interactable.transform.gameObject.name == _parentName;
    }

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {
        return interactable.transform.gameObject.name == _parentName;
    }
}
