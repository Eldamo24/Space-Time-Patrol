using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Player components")]
    private Transform playerPosition;
    private Rigidbody rbPlayer;
    private Transform playerBody;
    private PlayerInput playerInput;
    private Transform orientation;

    private Camera cam;

    private Transform startPosition;

    [Header("Player states")]
    private bool _isDead;
    public bool IsDead { get => _isDead; set => _isDead = value; }

    private int _playerLifes;
    public int PlayerLifes { get => _playerLifes; set => _playerLifes = value; }

    [Header("Movement and jump speed and force")]
    private Vector3 moveDirection;
    private Vector2 inputs;
    private float speedMovement = 10f;
    private float rotationSpeed = 10f;
    private float jumpForce = 6f;

    [Header("Collisions")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isCrushingBox;
    [SerializeField] private bool _isCrushingEnemy;
    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
    public bool IsCrushingBox { get => _isCrushingBox; set => _isCrushingBox = value; }
    public bool IsCrushingEnemy { get => _isCrushingEnemy; set => _isCrushingEnemy = value; }
    

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        orientation = GameObject.Find("Orientation").GetComponent<Transform>();
        playerPosition = GetComponent<Transform>();
        rbPlayer = GetComponent<Rigidbody>();
        playerBody = GetComponentInChildren<Transform>();
        playerInput = GetComponent<PlayerInput>();
        startPosition = GameObject.Find("StartPosition").GetComponent<Transform>(); ;
    }

    //Cambiar a fixedupdate al agregar new input system
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 viewDir = playerPosition.position - new Vector3(cam.transform.position.x, playerPosition.position.y, cam.transform.position.z);
        orientation.forward = viewDir.normalized;
        inputs = playerInput.actions["Movement"].ReadValue<Vector2>();
        Vector3 inputDir = orientation.forward * inputs.y + orientation.right * inputs.x;
        if (inputDir != Vector3.zero)
        {
            playerBody.forward = Vector3.Slerp(playerBody.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            rbPlayer.MovePosition(playerPosition.position + inputDir * Time.deltaTime * speedMovement);
        }
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (_isGrounded && GameManager.instance.GameStatus == GameStatus.Playing)
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

    public void Pause()
    {
        UIController.uiController.PauseGame();
    }

    public void PlayerDeath()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        _isDead = true;
        playerInput.enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        rbPlayer.useGravity = false;
    }

    public void ResetPlayer()
    {
        transform.position = startPosition.position;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        rbPlayer.useGravity = true;
        transform.GetChild(0).gameObject.SetActive(true);
        playerInput.enabled = true;
    }
}
