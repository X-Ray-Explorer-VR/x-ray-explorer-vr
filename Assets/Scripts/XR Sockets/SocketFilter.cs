using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SocketFilter : MonoBehaviour, IXRSelectFilter, IXRHoverFilter
{
    public bool canProcess => isActiveAndEnabled;
    [Header("Parameters")]
    public string gameObjectName;

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return interactable.transform.gameObject.name == gameObjectName;
    }

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {
        return interactable.transform.gameObject.name == gameObjectName;
    }
}
