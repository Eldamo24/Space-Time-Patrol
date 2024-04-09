using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerController.IsCrushingBox)
        {
            playerController.ReboundEffect();
            Destroy(gameObject);
        }
    }
}
