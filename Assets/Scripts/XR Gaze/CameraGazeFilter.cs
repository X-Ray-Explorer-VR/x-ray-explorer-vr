using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class CameraGazeFilter : MonoBehaviour, IXRHoverFilter
{
    public bool canProcess => isActiveAndEnabled;
    [Header("Properties")]
    [SerializeField]
    private string gazeInteractorName = "Hand Gaze Interactor";

    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        // Add filters
        simpleInteractable.hoverFilters.Add(this);
    }

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {
        return interactor.transform.name.Equals(gazeInteractorName);
    }
}
