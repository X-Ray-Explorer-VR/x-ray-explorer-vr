using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleTooltips : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private string tooltipTag = "Tooltip";
    [SerializeField]
    private string socketTag = "Socket";
    
    private XRGrabInteractable _interactable;
    private List<GameObject> _tooltips;

    private void Start()
    {
        _tooltips = new();
        
        _interactable = GetComponent<XRGrabInteractable>();
        // Get all available tooltips
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject child = transform.parent.GetChild(i).gameObject;
            
            if (child.gameObject.CompareTag(tooltipTag))
            {
                _tooltips.Add(child);
            }
        }
        // Add functions
        _interactable.selectEntered.AddListener(OnSelectEnter);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        Toggle(!args.interactorObject.transform.CompareTag(socketTag));
    }

    private void Toggle(bool visible)
    {
        foreach (var item in _tooltips)
        {
            SetAsRelevant setAsRelevant = item.GetComponent<SetAsRelevant>();

            if (!visible && setAsRelevant.isRelevant) continue;
            
            item.SetActive(visible);
        }
    }
}
