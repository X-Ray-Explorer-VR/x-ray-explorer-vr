using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SingleObjectHoverFilter : MonoBehaviour, IXRHoverFilter
{
    public bool canProcess => isActiveAndEnabled;

    private XRSocketInteractor socket;
    
    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
            
        // Automatically add the hover filter to the object
        socket.hoverFilters.Add(this);
    }

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {
        return !socket.hasSelection;
    }
}
