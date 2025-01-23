using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitchManager : MonoBehaviour
{
    public static glitchManager instance;
    public Material glitchMaterial;
    public float noiseAmount; //1000f
    public float glitchStrength; //1f

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

        private void Start()
    {
        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_NoiseAmt", noiseAmount);
            glitchMaterial.SetFloat("_GlitchStr", glitchStrength);
        }
    }

    public void EnableGlitchEffect()
    {
        noiseAmount = 1000f;
        glitchStrength = 1f;
        Debug.Log("Glitch effect enabled.");

        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_NoiseAmt", noiseAmount);
            glitchMaterial.SetFloat("_GlitchStr", glitchStrength);
        }
    }

    public void DisableGlitchEffect()
    {
        noiseAmount = 0f;
        glitchStrength = 0f;
        Debug.Log("Glitch effect disabled.");

        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_NoiseAmt", noiseAmount);
            glitchMaterial.SetFloat("_GlitchStr", glitchStrength);
        }
    }
}
