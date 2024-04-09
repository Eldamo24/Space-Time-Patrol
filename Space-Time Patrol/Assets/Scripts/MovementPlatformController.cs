using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatformController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private bool reachedPoint = true;
    private float speedMovement = 3f;
    private Rigidbody rbPlatform;

    private void Start()
    {
        rbPlatform = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!reachedPoint)
            rbPlatform.MovePosition(transform.position + Vector3.right * speedMovement * Time.deltaTime);
        else
            rbPlatform.MovePosition(transform.position + Vector3.left * speedMovement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        reachedPoint = !reachedPoint;
    }

}
