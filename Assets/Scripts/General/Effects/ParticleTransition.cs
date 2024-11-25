using UnityEngine;
using System.Collections;

public class ParticleTransition : MonoBehaviour
{
    public ParticleSystem ambParticleSystem;

    public void TriggerTransition()
    {
        StartCoroutine(TransitionEffect());
    }

    private IEnumerator TransitionEffect()
    {
        var forceOverLifetime = ambParticleSystem.forceOverLifetime;

        ParticleSystem.MinMaxCurve xForce = new ParticleSystem.MinMaxCurve(0);
        ParticleSystem.MinMaxCurve yForce = new ParticleSystem.MinMaxCurve(0);
        ParticleSystem.MinMaxCurve zForce = new ParticleSystem.MinMaxCurve(-10);

        forceOverLifetime.x = xForce;
        forceOverLifetime.y = yForce;
        forceOverLifetime.z = zForce;

        yield return new WaitForSeconds(1f);

        var main = ambParticleSystem.main;
        main.startSize = 100f;
    }
}
