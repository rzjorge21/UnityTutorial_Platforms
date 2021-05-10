using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = transform.position.y;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
