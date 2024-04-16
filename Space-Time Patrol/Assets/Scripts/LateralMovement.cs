using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LateralMovement : MonoBehaviour
{
    private bool reachedPoint = true;
    [SerializeField] private float speedMovement = 3f;
    private Rigidbody rbPlatform;
    private float smoothness = 30f;
    private Vector3 smoothedPosition;

    private void Start()
    {
        rbPlatform = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!reachedPoint)
        {
            lateralMovement(Vector3.right);
        }
        else
        {
            lateralMovement(Vector3.left);
        }
    }

    public void lateralMovement(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * speedMovement * Time.deltaTime;
        smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        rbPlatform.MovePosition(smoothedPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChangeDirection")
        {
            reachedPoint = !reachedPoint;
            if (gameObject.tag == "Enemy")
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
        }

    }
}
