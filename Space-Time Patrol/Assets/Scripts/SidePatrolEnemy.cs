using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SidePatrolEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().IsCrushingEnemy)
        {
            collision.gameObject.GetComponent<PlayerController>().ReboundEffect();
            Destroy(transform.parent.gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerDeath();
        }
    }
}
