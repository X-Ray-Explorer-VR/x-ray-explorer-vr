using System;
using UnityEngine;

public class ResetSkeleton : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField]
    private float resetTime = 1.0f;
    
    private Transform[] _bonesTransform;
    private Tuple<Vector3, Quaternion>[] _startPositions;
    private Tuple<Vector3, Quaternion>[] _initialAnimationsPosition;
    
    private bool _resetting;
    private float _currentTime;

    private void Start()
    {
        _resetting = false;
        
        _bonesTransform = gameObject.GetComponentsInChildren<Transform>();
        _startPositions = new Tuple<Vector3, Quaternion>[_bonesTransform.Length];

        for (int i = 0; i < _bonesTransform.Length; i++)
        {
            _startPositions[i] = new Tuple<Vector3, Quaternion>(_bonesTransform[i].position, _bonesTransform[i].rotation);
        }
    }

    private void FixedUpdate()
    {
        if (_resetting)
        {
            float animationPercent = Mathf.Clamp(_currentTime / resetTime, 0, 1.0f);
            
            for (int i = 0; i < _bonesTransform.Length; i++)
            {
                _bonesTransform[i].position = Vector3.Lerp(_initialAnimationsPosition[i].Item1, _startPositions[i].Item1, animationPercent);
                _bonesTransform[i].rotation =
                    Quaternion.Lerp(_initialAnimationsPosition[i].Item2, _startPositions[i].Item2, animationPercent);
            }

            if (animationPercent >= 1.0f)
            {
                _resetting = false;
                _currentTime = 0.0f;
            
                for (int i = 0; i < _bonesTransform.Length; i++)
                {

                    _bonesTransform[i].position = _startPositions[i].Item1;
                    _bonesTransform[i].rotation = _startPositions[i].Item2;
                }
            }
            
            _currentTime += Time.deltaTime;
        }
    }

    [ContextMenu("Reset bones position")]
    public void ResetPositions()
    {
        _initialAnimationsPosition = new Tuple<Vector3, Quaternion>[_bonesTransform.Length];
        
        for (int i = 0; i < _bonesTransform.Length; i++)
        {
            _initialAnimationsPosition[i] =
                new Tuple<Vector3, Quaternion>(_bonesTransform[i].position, _bonesTransform[i].rotation);
        }
        
        _resetting = true;
    }
}
