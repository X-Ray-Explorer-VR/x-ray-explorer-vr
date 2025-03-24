using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleGhostProjection : MonoBehaviour
{
    private MeshRenderer _bonesRender;
    private XRSocketInteractor _originalPositionSocket; 
        
    public void Start()
    {
        _bonesRender = transform.parent.GetComponent<MeshRenderer>();
        _originalPositionSocket = GetComponent<XRSocketInteractor>();
        
        // Hover events
        _originalPositionSocket.hoverEntered.AddListener(DisableProjection);
        _originalPositionSocket.hoverExited.AddListener(EnableProjection);
        // Select events
        _originalPositionSocket.selectEntered.AddListener(DisableProjection);
        _originalPositionSocket.selectExited.AddListener(EnableProjection);
    }

    private void OnDestroy()
    {
        _originalPositionSocket.hoverEntered.RemoveAllListeners();
        _originalPositionSocket.hoverExited.RemoveAllListeners();
        _originalPositionSocket.selectEntered.RemoveAllListeners();
        _originalPositionSocket.selectExited.RemoveAllListeners();
    }

    private void DisableProjection(SelectEnterEventArgs _)
    {
        _bonesRender.enabled = false;
    }

    private void DisableProjection(HoverEnterEventArgs _)
    {
        _bonesRender.enabled = false;
    }

    private void EnableProjection(SelectExitEventArgs _)
    {
        _bonesRender.enabled = true;
    }

    private void EnableProjection(HoverExitEventArgs _)
    {
        _bonesRender.enabled = true;
    }
}
