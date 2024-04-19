using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LateralMovement : MonoBehaviour
{
    private float duration = 5f;
    [SerializeField] private Transform firstTarget;
    [SerializeField] private Transform SecondTarget;


    private void Start()
    {
        StartCoroutine("LateralPatrol");
    }

    IEnumerator LateralPatrol()
    {
        while (true)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float escaleFactor = elapsedTime / duration;
                float smoothEscaleFactor = Mathf.SmoothStep(0f, 1f, escaleFactor);
                transform.position = Vector3.Lerp(transform.position, firstTarget.position, smoothEscaleFactor);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = firstTarget.position;
            transform.Rotate(new Vector3(0, 180, 0));
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float escaleFactor = elapsedTime / duration;
                float smoothEscaleFactor = Mathf.SmoothStep(0f, 1f, escaleFactor);
                transform.position = Vector3.Lerp(transform.position, SecondTarget.position, smoothEscaleFactor);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = SecondTarget.position;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void OnDestroy()
    {
        StopCoroutine("LateralPatrol");
    }
}
