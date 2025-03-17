using System;
using UnityEngine;

public class ResetSkeleton : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField]
    private float resetTime = 1.0f;
    
    private Transform[] bonesTransforms;
    private Tuple<Vector3, Quaternion>[] startPosition;
    private Tuple<Vector3, Quaternion>[] initialAnimationPosition;
    
    private bool resetting;

    private float currentTime;

    private void Start()
    {
        resetting = false;
        
        bonesTransforms = gameObject.GetComponentsInChildren<Transform>();
        startPosition = new Tuple<Vector3, Quaternion>[bonesTransforms.Length];

        for (int i = 0; i < bonesTransforms.Length; i++)
        {
            startPosition[i] = new Tuple<Vector3, Quaternion>(bonesTransforms[i].position, bonesTransforms[i].rotation);
        }
    }

    private void FixedUpdate()
    {
        if (resetting)
        {
            float animationPercent = Mathf.Clamp(currentTime / resetTime, 0, 1.0f);
            
            for (int i = 0; i < bonesTransforms.Length; i++)
            {
                bonesTransforms[i].position = Vector3.Lerp(initialAnimationPosition[i].Item1, startPosition[i].Item1, animationPercent);
                bonesTransforms[i].rotation =
                    Quaternion.Lerp(initialAnimationPosition[i].Item2, startPosition[i].Item2, animationPercent);
            }

            if (animationPercent >= 1.0f)
            {
                resetting = false;
                currentTime = 0.0f;
            
                for (int i = 0; i < bonesTransforms.Length; i++)
                {

                    bonesTransforms[i].position = startPosition[i].Item1;
                    bonesTransforms[i].rotation = startPosition[i].Item2;
                }
            }
            
            currentTime += Time.deltaTime;
        }
    }

    [ContextMenu("Reset bones position")]
    public void ResetPositions()
    {
        initialAnimationPosition = new Tuple<Vector3, Quaternion>[bonesTransforms.Length];
        
        for (int i = 0; i < bonesTransforms.Length; i++)
        {
            initialAnimationPosition[i] =
                new Tuple<Vector3, Quaternion>(bonesTransforms[i].position, bonesTransforms[i].rotation);
        }
        
        resetting = true;
    }
}
