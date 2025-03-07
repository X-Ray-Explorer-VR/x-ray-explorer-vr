using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowAndHideTooltips : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private string tooltipTag = "Tooltip";
    [SerializeField]
    private string socketTag = "Socket";
    
    private XRGrabInteractable interactable;
    private List<GameObject> tooltips;

    private void Start()
    {
        tooltips = new();
        
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
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        ToggleTooltips(!args.interactorObject.transform.CompareTag(socketTag));
    }

    private void ToggleTooltips(bool visible)
    {
        foreach (var item in tooltips)
        {
            item.SetActive(visible);
        }
    }
}
