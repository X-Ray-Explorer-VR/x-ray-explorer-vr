using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField] private Transform resetTransform;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerHead;
    
    [ContextMenu("Reset position")]
    void Start()
    {
        float rotationAngleY = resetTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0.0f, rotationAngleY, 0.0f);

        Vector3 distanceDiff = resetTransform.position - playerHead.transform.position;
        distanceDiff.y = 0.0f;
        player.transform.position += distanceDiff;
    }
}
