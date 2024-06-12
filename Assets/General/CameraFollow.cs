using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    public float smoothing = 1f;
    public Vector2 minPosition = new Vector2(-5, -5);
    public Vector2 maxPosition= new Vector2(5, 5);
 
    public void Init(Transform target)
    {
        _target = target;
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    private void Start()
    {
        // _offset = new Vector3(0, 0, -10);
    }
 
    void LateUpdate()
    {
        if (transform.position != _target.position)
        {
            Vector3 targetPosition = new Vector3(_target.position.x,
                _target.position.y,
                transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x,
                minPosition.x,
                maxPosition.x);

            targetPosition.y = Mathf.Clamp(targetPosition.y,
                minPosition.y,
                maxPosition.y);

            transform.position = Vector3.Lerp(transform.position,
                targetPosition, smoothing);
        }
    }
}
