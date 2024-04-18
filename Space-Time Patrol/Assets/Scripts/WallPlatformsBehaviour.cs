using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WallPlatformsBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private float finalScaleX = 5f;
    private float duration = 2f;
    private Vector3 initialScale;
    private Vector3 finalScale;
    private Coroutine scalePlatformCoroutine;
    private float waitTime;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        waitTime = Random.Range(3f, 5f);
        initialScale = transform.localScale;
        finalScale = new Vector3(finalScaleX, transform.localScale.y, transform.localScale.z);
        scalePlatformCoroutine = StartCoroutine("ScalePlatform");
    }


    IEnumerator ScalePlatform()
    {
        yield return new WaitForSecondsRealtime(Random.Range(3f, 5f));
        while (true)
        {
            float elapsedTime = 0;
            while(elapsedTime < duration)
            {
                float escaleFactor = elapsedTime / duration;
                transform.localScale = Vector3.Lerp(transform.localScale, finalScale, escaleFactor);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = finalScale;
            yield return new WaitForSecondsRealtime(waitTime);
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float escaleFactor = elapsedTime / duration;
                transform.localScale = Vector3.Lerp(transform.localScale, initialScale, escaleFactor);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = initialScale;
            yield return new WaitForSecondsRealtime(waitTime);

        }
    }

    private void OnDestroy()
    {
        StopCoroutine(scalePlatformCoroutine);
    }
}
