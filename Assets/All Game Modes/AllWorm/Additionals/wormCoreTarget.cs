using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class wormCoreTarget : MonoBehaviour
{
    public float revealDuration = 3f;
    private bool isRevealed = false;
    public GameObject shieldObject;
    public int shieldHealth = 10;
    public GameObject vulnerableObject;
    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    private Transform parentTransform;
    public survivalSoundSource sfx;

    public GameObject normScreen;
    public GameObject alertObj;

    private void Start()
    {
        shieldObject.SetActive(false);
        parentTransform = transform.parent;
    }

    public void RevealEnemy()
    {
        isRevealed = true;
        sfx.revealSFX();
        shieldObject.SetActive(true);
        Invoke("ResetRevealState", revealDuration);
        alertObj.SetActive(true);
        normScreen.SetActive(false);
    }

    public bool IsRevealed()
    {
        return isRevealed;
    }

    private void ResetRevealState()
    {
        isRevealed = false;
        shieldObject.SetActive(false);
        alertObj.SetActive(false);
        normScreen.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scanner"))
        {
            RevealEnemy();
        }

        if (other.CompareTag("Player Bullet") && isRevealed)
        {
            if (shieldHealth > 0)
            {
                shieldHealth--;
                hitFX.Play();
                sfx.hitSFX();
                Debug.Log("shield hit");
                if (shieldHealth <= 0)
                {
                    Destroy(shieldObject);
                    hitFX.Play();
                    sfx.hitSFX();
                    vulnerableObject.SetActive(true);
                    Debug.Log("core open");
                }
            }
            else
            {
                sparksFX.Play();
                smokeFX.Play();
                flashFX.Play();
                fireFX.Play();
                sfx.explosionSFX();
                alertObj.SetActive(false);
                normScreen.SetActive(true);
                Destroy(vulnerableObject);
                if (parentTransform != null)
                {
                    Destroy(parentTransform.gameObject);
                }
            }
        }
    }
}
