using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{


    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    private void Update()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 position = target.position;
        Vector3 center = position + direction * (Camera.main.nearClipPlane + 0.2f);

        Vector3 right = transform.right * Camera.main.aspect;
        Vector3 up = transform.up;

        return new Vector3[]
        {
            center - right + up,
            center + right + up,
            center - right - up,
            center + right - up
        };
    }

    void LateUpdate()
    {
        Vector3 direction = transform.position - target.position;

        RaycastHit hit;
        float distance = direction.magnitude;
        Vector3[] points = GetCameraCollisionPoints(direction.normalized);

        foreach (Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out hit))
            {
                distance = Mathf.Min(hit.distance, distance);
            }
        }

        transform.position = target.position + direction.normalized * distance;
    }

}
