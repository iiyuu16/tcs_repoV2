using UnityEngine;
using System.Collections;
public class flappyCollisionManager : MonoBehaviour
{
    //explosionFX
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    public Renderer plyrRender;

    public flappySoundSource soundSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("player dead");
            sparksFX.Play();
            smokeFX.Play();
            fireFX.Play();
            plyrRender.enabled = false;
            StartCoroutine(DelayDestroy());
        }
        else if (other.gameObject.tag == "Waypoint")
        {
            Debug.Log("player scored");
            soundSource.scoreSFX();
        }
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
