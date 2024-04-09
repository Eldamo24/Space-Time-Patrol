using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatformController : MonoBehaviour
{
    private bool reachedPoint = true;
    private float speedMovement = 3f;
    private Rigidbody rbPlatform;
    private float smoothness = 30f;

    private void Start()
    {
        rbPlatform = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!reachedPoint)
        {
            Vector3 targetPosition = transform.position + Vector3.right * speedMovement * Time.deltaTime;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
            rbPlatform.MovePosition(smoothedPosition);
        }
        else
        {
            Vector3 targetPosition = transform.position + Vector3.left * speedMovement * Time.deltaTime;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
            rbPlatform.MovePosition(smoothedPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        reachedPoint = !reachedPoint;
    }

}
