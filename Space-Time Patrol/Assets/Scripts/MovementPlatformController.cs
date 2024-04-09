using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            MovePlatform(Vector3.right);
        }
        else
        {
            MovePlatform(Vector3.left);
        }
    }

    private void MovePlatform(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * speedMovement * Time.deltaTime;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        rbPlatform.MovePosition(smoothedPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        reachedPoint = !reachedPoint;
    }
}
