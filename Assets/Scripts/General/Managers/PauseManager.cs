using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    private AudioListener audioListener;
    public AudioSource audioSource;

    private void Awake()
    {
        audioListener = FindObjectOfType<AudioListener>();
    }

    public void PauseGame()
    {
        Pause();
    }

    public void UnpauseGame()
    {
        Unpause();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        audioSource.enabled = false; // Pause audio
    }

    private void Unpause()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        audioSource.enabled = true; // Unpause audio
    }
}