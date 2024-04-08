using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerPosition;
    private Rigidbody rbPlayer;
    private Transform playerBody;
    private Vector3 moveDirection;

    [SerializeField] private Transform checkGround;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;

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
        isGrounded = Physics.CheckSphere(checkGround.position, 0.2f, groundLayer);
        Movement();
        Jump();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, 0.2f);
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
