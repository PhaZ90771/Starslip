using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    public Vector2 limits = new Vector3(5, 3);
    [Range(0, 1)]
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.localPosition = offset;
        FollowTarget(target);
    }

    private void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    }

    private void FollowTarget(Transform t)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetPos = t.localPosition;
        Vector3 currentVelocity = new Vector3(targetPos.x + offset.x, targetPos.y + offset.y, localPos.z);
        transform.localPosition = Vector3.SmoothDamp(localPos, currentVelocity, ref velocity, smoothTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(limits.x, -limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(-limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(limits.x, -limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
    }
}
