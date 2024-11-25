using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mainSoundSource : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1, sfx2, sfx3;

    public void click1()
    {
        src.clip = sfx1;
        src.Play();
    }
    public void click2()
    {
        src.clip = sfx2;
        src.Play();
    }

    public void click3()
    {
        src.clip = sfx3;
        src.Play();
    }
}
