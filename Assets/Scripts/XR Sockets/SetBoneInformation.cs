using System;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetBoneInformation : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scientificNameText;
    public TextMeshProUGUI descriptionText;
    public XRSocketInteractor socket;

    public void UpdateInformation()
    {
        GameObject objectInSocket = socket.GetOldestInteractableSelected().transform.gameObject;

        try
        {
            string boneCode = objectInSocket.GetComponent<BoneCode>().code;
            Debug.Log(boneCode);

            if (boneCode.IsNotNullOrEmpty())
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
        switch (boneCode)
        {
            case "skull":
                nameText.text = "Cráneo";
                scientificNameText.text = "cranium";
                descriptionText.text = "El cráneo proporciona un medio de proteción al encéfalo y a órganos sensoriales especiales (vista, olfato, equilibrio y gusto).\n\nEste se compone de 8 huesos diferentes como el hueso frontal, maxilar superior, temporal izquierdo y derecho, pariental izquierdo y derecho, occipital, cigomático izquierdo y derecho, esfenoides y la mandíbula.";
                break;
            case "left-femur":
            default:
                nameText.text = "Fémur izquierdo";
                scientificNameText.text = "os femuris";
                descriptionText.text = "El fémur es el hueso más largo y fuerte del cuerpo y soporta el peso corporal al ponerse de pie, caminar y saltar.\n\nEn la rodilla, el fémur y la tibia se transladan uno sobre otro como una bisagra, lo que provoca la flexión y al extensión.";
                break;

        }
    }
}
