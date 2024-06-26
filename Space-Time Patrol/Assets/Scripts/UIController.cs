using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public static UIController uiController;
    private GameObject levelData;
    private Transform clocksPosition, boxesPosition, lifesPosition;
    private Transform targetClocksPosition, targetBoxesPosition, targetLifesPosition;
    private Vector3 initialClocksPosition, initialBoxesPosition, initialLifesPosition;
    private TMP_Text clocksText, boxesText, lifesText;
    private GameObject pausePanel;
    private Coroutine moveIn, moveOut;
    private float duration;
    private int lifes, clocks, actualBoxes, totalBoxes;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        uiController = this;
        levelData = GameObject.Find("LevelData");
        clocksText = GameObject.Find("Clocks").GetComponent<TMP_Text>();
        boxesText = GameObject.Find("Boxes").GetComponent<TMP_Text>();
        lifesText = GameObject.Find("Lifes").GetComponent<TMP_Text>();
        clocksPosition = clocksText.gameObject.transform;
        boxesPosition = boxesText.gameObject.transform;
        lifesPosition = lifesText.gameObject.transform;
        targetClocksPosition = GameObject.Find("ClocksTarget").GetComponent<Transform>();
        targetBoxesPosition = GameObject.Find("BoxesTarget").GetComponent<Transform>();
        targetLifesPosition = GameObject.Find("LifesTarget").GetComponent<Transform>();
        initialClocksPosition = clocksPosition.position;
        initialBoxesPosition = boxesPosition.position;
        initialLifesPosition = lifesPosition.position;
        duration = 2f;
        isMoving = false;
        pausePanel = GameObject.Find("PausePanel");
        pausePanel.SetActive(false);
        InitializeUI();
    }

    public void CallUILevel(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (!isMoving && GameManager.instance.GameStatus == GameStatus.Playing)
            {
                levelData.SetActive(true);
                moveIn = StartCoroutine("MoveInUI");
            }
        }
    }

    IEnumerator MoveInUI()
    {
        isMoving = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            clocksPosition.position = Vector3.Lerp(clocksPosition.position, targetClocksPosition.position, (elapsedTime / duration));
            boxesPosition.position = Vector3.Lerp(boxesPosition.position, targetBoxesPosition.position, (elapsedTime / duration));
            lifesPosition.position = Vector3.Lerp(lifesPosition.position, targetLifesPosition.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        clocksPosition.position = targetClocksPosition.position;
        boxesPosition.position = targetBoxesPosition.position;
        lifesPosition.position = targetLifesPosition.position;
        yield return new WaitForSecondsRealtime(2f);
        moveOut = StartCoroutine("MoveOutUI");
    }

    IEnumerator MoveOutUI()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            clocksPosition.position = Vector3.Lerp(clocksPosition.position, initialClocksPosition, (elapsedTime / duration));
            boxesPosition.position = Vector3.Lerp(boxesPosition.position, initialBoxesPosition, (elapsedTime / duration));
            lifesPosition.position = Vector3.Lerp(lifesPosition.position, initialLifesPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RestartLevelUIPosition();
        isMoving = false;
        yield return null;
    }

    private void InitializeUI()
    {
        clocks = 0;
        lifes = FindObjectOfType<PlayerController>().PlayerLifes;
        actualBoxes = 0;
        totalBoxes = GameObject.FindGameObjectsWithTag("Box").Length;
        clocksText.text = "Clocks: " + clocks;
        lifesText.text = "Lifes: " + lifes;
        boxesText.text = "Boxes: " + actualBoxes + "/" + totalBoxes;
    }

    public void SetLifes()
    {
        lifes = FindObjectOfType<PlayerController>().PlayerLifes;
        UpdateUI();
    }

    public void SetBoxes()
    {
        actualBoxes++; ;
        UpdateUI();
    }

    public void SetClocks()
    {
        clocks++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        clocksText.text = "Clocks: " + clocks;
        lifesText.text = "Lifes: " + lifes;
        boxesText.text = "Boxes: " + actualBoxes + "/" + totalBoxes;
    }

    public void PauseGame()
    {
        if(GameManager.instance.GameStatus == GameStatus.Paused)
        {
            GameManager.instance.GameStatus = GameStatus.Playing;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
        else
        {
            GameManager.instance.GameStatus = GameStatus.Paused;
            StopAllCoroutines();
            RestartLevelUIPosition();
            isMoving = false;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        PauseGame();
    }

    private void RestartLevelUIPosition()
    {
        clocksPosition.position = initialClocksPosition;
        boxesPosition.position = initialBoxesPosition;
        lifesPosition.position = initialLifesPosition;
    }
}
