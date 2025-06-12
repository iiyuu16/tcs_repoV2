using UnityEngine;

public class mazeSoundSource : MonoBehaviour
{
    public AudioSource src;
    public AudioClip dead;

    public void deadSFX()
    {
        src.PlayOneShot(dead);
    }
}
