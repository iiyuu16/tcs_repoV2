using UnityEngine;

public class flappySoundSource : MonoBehaviour
{
    public AudioSource src;
    public AudioClip dead, score, jump;

    public void deadSFX()
    {
        src.PlayOneShot(dead);
    }

    public void scoreSFX()
    {
        src.PlayOneShot(score);
    }

    public void jumpsSFX()
    {
        src.PlayOneShot(jump);
    }
}
