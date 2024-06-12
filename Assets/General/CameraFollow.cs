using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    public float smoothing = 1f;
 
    public void Init(Transform target)
    {
        _target = target;
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
 
    void LateUpdate()
    {
        if (transform.position != _target.position)
        {
            Vector3 targetPosition = new Vector3(_target.position.x,
                _target.position.y,
                transform.position.z);
            
            transform.position = Vector3.Lerp(transform.position,
                targetPosition, smoothing);
        }
    }
}