using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public Transform target;            // What will the camera be following? allows for selection of Player obj
    public float smoothing = 20.0f;        // The speed with which the camera will be following.
    private Vector3 velocity = Vector3.zero;

    Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        // Tell the camera where to tatget while the ball is moving
        Vector3 targetCamPos = target.position + offset;

        targetCamPos.y = 0;

        // Smoothly move between the cameras current position and the target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Time.deltaTime * smoothing);
    }
}


