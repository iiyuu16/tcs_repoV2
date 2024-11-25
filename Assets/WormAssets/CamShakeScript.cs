using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public Transform camTransform;


    public float shakeDuration = 0f;


    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public GameObject hit;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            hit.SetActive(true);

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            hit.SetActive(false);
            camTransform.localPosition = originalPos;
        }
    }
}
