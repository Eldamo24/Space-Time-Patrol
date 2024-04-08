using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerPosition;
    private Rigidbody rbPlayer;
    private Transform playerBody;
    private Vector3 moveDirection;
    private float speedMovement = 10f;
    private float rotationSpeed = 10f;
    private float jumpForce = 5f;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GetComponent<Transform>();
        rbPlayer = GetComponent<Rigidbody>();
        playerBody = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector3(horizontal, 0, vertical);
        if (moveDirection != Vector3.zero)
        {
            Quaternion playerBodyRotation = Quaternion.LookRotation(moveDirection);
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, playerBodyRotation, rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        rbPlayer.MovePosition(playerPosition.position + moveDirection * speedMovement * Time.deltaTime);
    }
}
