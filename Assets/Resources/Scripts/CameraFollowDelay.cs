using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowDelay : MonoBehaviour {

    public Transform target;

    [SerializeField, Range(1f, 20f)]
    public float _distance;

    [SerializeField, Range(1f, 20f)]
    public float offsetY;

    private Vector3 _offset;
        
    private Vector3 _lookDown = new Vector3(10f, 0f, 0f);
    private const float _followRate = 0.1f;

    void Start()
    {
        _offset = new Vector3(0f, offsetY, -_distance);

        transform.position = target.TransformPoint(_offset);
        transform.LookAt(target, Vector3.up);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.TransformPoint(_offset);
        Vector3 lerp = Vector3.Lerp(transform.position, desiredPosition, _followRate);
        Vector3 toTarget = target.position - lerp;
        toTarget.Normalize();
        toTarget *= _distance;
        transform.position = target.position - toTarget;
        transform.LookAt(target, Vector3.up);
        transform.Rotate(_lookDown);
    }
}
