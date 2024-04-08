using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player components")]
    private Transform playerPosition;
    private Rigidbody rbPlayer;
    private Transform playerBody;
    
    [Header("Movement and jump speed and force")]
    private Vector3 moveDirection;
    private float speedMovement = 10f;
    private float rotationSpeed = 10f;
    private float jumpForce = 5f;

    [Header("Collisions")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isCrushingBox;
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsCrushingBox { get => isCrushingBox; set => isCrushingBox = value; }



    void Start()
    {
        playerPosition = GetComponent<Transform>();
        rbPlayer = GetComponent<Rigidbody>();
        playerBody = GetComponentInChildren<Transform>();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector3(horizontal, 0, vertical);
        if (moveDirection != Vector3.zero)
        {
            Quaternion playerBodyRotation = Quaternion.LookRotation(moveDirection);
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, playerBodyRotation, rotationSpeed * Time.deltaTime);
        }
        rbPlayer.MovePosition(playerPosition.position + moveDirection * speedMovement * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
