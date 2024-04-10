using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject levelData;
    private Transform clocksPosition, boxesPosition, lifesPosition;
    private Transform targetClocksPosition, targetBoxesPosition, targetLifesPosition;
    private Vector3 initialClocksPosition, initialBoxesPosition, initialLifesPosition;
    private TMP_Text clocksText, boxesText, lifesText;
    private float duration;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isMoving)
        {
            levelData.SetActive(true);
            StartCoroutine("MoveInUI");
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
        StartCoroutine("MoveOutUI");
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
        clocksPosition.position = initialClocksPosition;
        boxesPosition.position = initialBoxesPosition;
        lifesPosition.position = initialLifesPosition;
        isMoving = false;
        yield return null;
    }
}
