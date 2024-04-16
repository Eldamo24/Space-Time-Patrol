using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SidePatrolEnemy : MonoBehaviour, IEnemy
{
    public int Life { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Life = -1;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().IsCrushingEnemy)
        {
            collision.gameObject.GetComponent<PlayerController>().ReboundEffect();
            Destroy(gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerDeath();
        }
    }
}
