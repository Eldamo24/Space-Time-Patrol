using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask boxLayer;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        playerController.IsGrounded = Physics.CheckSphere(checkGround.position, 0.2f, groundLayer);
        playerController.IsCrushingBox = Physics.CheckSphere(checkGround.position, 0.2f, boxLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, 0.2f);
    }
}
