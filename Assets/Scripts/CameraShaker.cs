using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private static CameraShaker instance;
    public static CameraShaker Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CameraShaker>();
            return instance;
        }
    }

    private Coroutine shakeCoroutine;

    public void Shake(float amount, float duration)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);
        shakeCoroutine = StartCoroutine(ShakeCo(amount, duration));
    }

    private IEnumerator ShakeCo(float amount, float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = (Vector3)Random.insideUnitCircle * amount;
            yield return null;
        }
        transform.position = Vector3.zero;
    }
}
