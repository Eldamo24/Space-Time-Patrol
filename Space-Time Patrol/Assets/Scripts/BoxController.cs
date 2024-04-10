using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private PlayerController playerController;
    private UIController uiController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiController = FindObjectOfType<UIController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerController.IsCrushingBox)
        {
            playerController.ReboundEffect();
            UIController.uiController.SetBoxes();
            Destroy(gameObject);
        }
    }
}
