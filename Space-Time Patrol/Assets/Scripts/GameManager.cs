using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameStatus _gameStatus;
    private PlayerController playerController;
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private LayerMask enemyLayer;

    public GameStatus GameStatus { get => _gameStatus; set => _gameStatus = value; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerController = FindObjectOfType<PlayerController>();
        playerController.PlayerLifes = 3;
        playerController.IsDead = false;
        _gameStatus = GameStatus.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisions();
        if (playerController.IsDead)
        {
            if(playerController.PlayerLifes > 0)
            {
                playerController.PlayerLifes--;
                UIController.uiController.SetLifes();
                playerController.IsDead = false;
                StartCoroutine("ResetLevel");
            }
            else
            {
                StartCoroutine("RestartGame");
            }
        }
            
    }

    private void CheckCollisions()
    {
        playerController.IsGrounded = Physics.CheckSphere(checkGround.position, 0.2f, groundLayer);
        playerController.IsCrushingEnemy = Physics.CheckSphere(checkGround.position, 0.2f, enemyLayer);
        RaycastHit hit;
        if(Physics.Raycast(checkGround.position, Vector3.down, out hit, 0.2f,  groundLayer))
        {
            Debug.DrawRay(checkGround.position, Vector3.down);
            if(hit.transform.tag == "Box")
            {
                playerController.IsCrushingBox = true;
            }
            else
            {
                playerController.IsCrushingBox = false;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, 0.2f);
    }

    public void SetGameStatus(GameStatus status)
    {
        _gameStatus = status;
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSecondsRealtime(3f);
        playerController.ResetPlayer();
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("Prototype");
    }
}
