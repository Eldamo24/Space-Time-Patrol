using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Transform playerBody;
    private Vector3 moveDirection;
    private float speedMovement = 10f;
    private float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerBody = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector3(horizontal, 0, vertical);
        characterController.Move(moveDirection * speedMovement * Time.deltaTime);
        if(moveDirection != Vector3.zero)
        {
            Quaternion playerBodyRotation = Quaternion.LookRotation(moveDirection);
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, playerBodyRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
