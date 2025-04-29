using UnityEngine;
using System.Collections;

public class BlinkingImage : MonoBehaviour
{
    public float blinkInterval = 0.5f;

    void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkInterval);

            gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkInterval);
    }
}
