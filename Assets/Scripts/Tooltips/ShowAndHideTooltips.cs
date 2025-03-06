using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowAndHideTooltips : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private string tooltipTag = "Tooltip";
    
    private XRGrabInteractable interactable;
    private List<GameObject> tooltips = new();

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        // Get all available tooltips
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            
            if (child.gameObject.CompareTag(tooltipTag))
            {
                tooltips.Add(child);
            }
        }
        // Add functions
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        ToggleTooltips(true);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        ToggleTooltips(false);
    }

    private void ToggleTooltips(bool visible)
    {
        foreach (var item in tooltips)
        {
            item.SetActive(visible);
        }
    }
}
