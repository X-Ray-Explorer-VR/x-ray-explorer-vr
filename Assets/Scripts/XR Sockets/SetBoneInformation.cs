using System;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetBoneInformation : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scientificNameText;
    public TextMeshProUGUI descriptionText;
    public XRSocketInteractor socket;

    private XmlDocument _boneData;

    private void Start()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Bone Data");

        _boneData = new XmlDocument();
        _boneData.PreserveWhitespace = false;
        _boneData.LoadXml(textAsset.text);
    }

    public void UpdateInformation()
    {
        GameObject objectInSocket = socket.GetOldestInteractableSelected().transform.gameObject;

        try
        {
            string boneCode = objectInSocket.GetComponent<BoneCode>().code;

            if (!string.IsNullOrEmpty(boneCode))
            {
                SetInformation(boneCode);
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    private void SetInformation(string boneCode)
    {
        XmlNode node = _boneData.DocumentElement.SelectSingleNode(boneCode);

        // Get data from node attributes
        nameText.text = node.Attributes["name"].InnerText;
        scientificNameText.text = node.Attributes["scientific-name"].InnerText;
        descriptionText.text = node.Attributes["description"].InnerText.Replace("\\n", "\n");
    }
}
