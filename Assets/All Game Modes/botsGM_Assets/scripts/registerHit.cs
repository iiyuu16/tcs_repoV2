using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registerHit : MonoBehaviour
{
    public GameObject objMesh;
    public survivalSoundSource sfx;

    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            objMesh.SetActive(false);
            sparksFX.Play();
            smokeFX.Play();
            flashFX.Play();
            fireFX.Play();
            sfx.explosionSFX();
            StartCoroutine(DelayDisable());
        }
    }

    private IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}