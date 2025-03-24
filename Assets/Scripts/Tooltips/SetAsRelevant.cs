using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SetAsRelevant : MonoBehaviour
{
    [Header("State")]
    public bool isRelevant = false;

    [Header("Properties")]
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color relevantColor;
    [Header("References")]
    [SerializeField]
    private Image borderImage;
    [SerializeField]
    private LineRenderer line;

    private XRGrabInteractable _xrGrabInteractable;

    private void Start()
    {
        _xrGrabInteractable = transform.parent.GetComponentInChildren<XRGrabInteractable>();
    }

    public void SetRelevant(bool value)
    {
        if (value)
        {
            borderImage.color = relevantColor;
            line.startColor = relevantColor;
            line.endColor = relevantColor;
            isRelevant = true;
            
            gameObject.SetActive(true);
        }
        else
        {
            borderImage.color = defaultColor;
            line.startColor = defaultColor;
            line.endColor = defaultColor;
            isRelevant = false;
            
            gameObject.SetActive(false);
            
        }
    }
}
