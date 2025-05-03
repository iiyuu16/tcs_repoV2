using System.Collections;
using TMPro;
using UnityEngine;

public class GlitchText3D : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Material originalMaterial;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        originalMaterial = textMesh.fontMaterial;
        StartCoroutine(GlitchEffect());
    }

    IEnumerator GlitchEffect()
    {
        while (true)
        {
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, Random.Range(0.0f, 1.0f));
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, Random.Range(0.0f, 0.1f));

            if (Random.value < 0.2f)
            {
                float italicFactor = Random.Range(-0.2f, 0.2f);
                textMesh.fontStyle = FontStyles.Italic;
                textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_ScaleX, 1 + italicFactor);

                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

                textMesh.fontStyle = FontStyles.Normal;
                textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_ScaleX, 1f);
            }

            yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));
        }
    }
}
