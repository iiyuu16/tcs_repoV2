using System.Collections;
using UnityEngine;

public class ryhthmManualPulse : MonoBehaviour
{
    public float pulseSize = 1.2f;
    public float pulseSpeed = 1f;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PulseEffect());
        }
    }

    private IEnumerator PulseEffect()
    {
        float t = 0f;
        while (t < 1f)
        {
            float scaleFactor = Mathf.Lerp(1f, pulseSize, t);
            transform.localScale = originalScale * scaleFactor;
            t += Time.deltaTime * pulseSpeed;
            yield return null;
        }
    }
}
