using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            FindObjectOfType<PlayerController>().PlayerLifes++;
            UIController.uiController.SetLifes();
            Destroy(gameObject);
        }
    }
}
