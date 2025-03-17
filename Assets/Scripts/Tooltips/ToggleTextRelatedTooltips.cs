using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleTextRelatedTooltips : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private string tooltipTag = "Tooltip";
    [SerializeField]
    private string skeletonTag = "Skeleton";
    [SerializeField]
    private string[] specialWordsToCheck =
    {
        "derecha",
        "izquierda",
        "derecho",
        "izquierdo",
        "hueso"
    };

    private GameObject skeleton;
    private List<Tuple<string, SetAsRelevant>> tooltips;

    private void Start()
    {
        tooltips = new List<Tuple<string, SetAsRelevant>>();

        skeleton = GameObject.FindGameObjectWithTag(skeletonTag);

        for (var i = 0; i < skeleton.transform.childCount; i++)
        {
            GameObject bone = skeleton.transform.GetChild(i).gameObject;

            if (bone.name != "Sockets")
            {
                for (int j = 0; j < bone.transform.childCount; j++)
                {
                    GameObject boneChild = bone.transform.GetChild(j).gameObject;

                    if (boneChild.CompareTag(tooltipTag))
                    {
                        string tooltipName = boneChild.name.ToLower().Split('(', ')')[1];

                        // Remove special words
                        foreach (var item in specialWordsToCheck)
                        {
                            if (tooltipName.Contains(item))
                            {
                                tooltipName = tooltipName.Replace(item, "").Trim();
                            }
                        }
                        
                        tooltips.Add(new Tuple<string, SetAsRelevant>(tooltipName, boneChild.GetComponent<SetAsRelevant>()));
                    }
                }
            }
        }
    }
    
    public void ShowRelatedTooltips(string text)
    {
        string lowerText = text.ToLower();
        
        foreach (var tooltip in tooltips)
        {
            if (lowerText.Contains(tooltip.Item1))
            {
                tooltip.Item2.SetRelevant(true);
            }
        }
    }

    [ContextMenu("Hide all tooltips")]
    public void HideAllTooltips()
    {
        foreach (var tooltip in tooltips)
        {
            if (tooltip.Item2.isRelevant)
            {
                tooltip.Item2.SetRelevant(false);
            }
        }
    }
}
