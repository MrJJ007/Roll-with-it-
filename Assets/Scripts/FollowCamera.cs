using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.1F;

    private Vector3 offset;
    public float offsetX;//change these to offset ignore the one above
    public float offsetY;
    public float offsetZ;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        offset = new Vector3(offsetX, offsetY, offsetZ);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        //float yRotation = target.transform.eulerAngles.y;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

    }
}
