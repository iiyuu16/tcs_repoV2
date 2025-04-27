using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plyrWormBullet : MonoBehaviour
{
    public float bulletLife = 5f;
    public Renderer bulletSkin;

    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    public Collider col;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield"||other.tag == "Target"|| other.tag == "Obstacle" || other.tag == "Turret")
        {
            sparksFX.Play();
            smokeFX.Play();
            fireFX.Play();
            StartCoroutine(DelayDestroy());
        }
        else
        {
            StartCoroutine(BulletLifetime());
        }
    }

    private IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(gameObject);
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
