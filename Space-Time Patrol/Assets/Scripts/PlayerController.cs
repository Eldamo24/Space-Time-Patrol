using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player components")]
    private Transform playerPosition;
    private Rigidbody rbPlayer;
    private Transform playerBody;
    private PlayerInput playerInput;


    [Header("Movement and jump speed and force")]
    private Vector3 moveDirection;
    private Vector2 inputs;
    private float speedMovement = 10f;
    private float rotationSpeed = 10f;
    private float jumpForce = 5f;

    [Header("Collisions")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isCrushingBox;
    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
    public bool IsCrushingBox { get => _isCrushingBox; set => _isCrushingBox = value; }



    void Start()
    {
        playerPosition = GetComponent<Transform>();
        rbPlayer = GetComponent<Rigidbody>();
        playerBody = GetComponentInChildren<Transform>();
        playerInput = GetComponent<PlayerInput>();
    }

    //Cambiar a fixedupdate al agregar new input system
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        inputs = playerInput.actions["Movement"].ReadValue<Vector2>();
        moveDirection = new Vector3(inputs.x, 0, inputs.y);
        if (moveDirection != Vector3.zero)
        {
            Quaternion playerBodyRotation = Quaternion.LookRotation(moveDirection);
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, playerBodyRotation, rotationSpeed * Time.deltaTime);
        }
        rbPlayer.MovePosition(playerPosition.position + moveDirection * speedMovement * Time.deltaTime);
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (_isGrounded)
            {
                rbPlayer.velocity = Vector3.zero;
                rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }


    public void ReboundEffect()
    {
        rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
