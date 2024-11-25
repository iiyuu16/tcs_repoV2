using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkingEffect());
    }

    IEnumerator BlinkingEffect()
    {
        while (true)
        {
            textMesh.enabled = !textMesh.enabled;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
