using UnityEngine;
using TMPro;

public class FirstButton : MonoBehaviour
{
    public TextMeshProUGUI pressToContinueText;
    public GameObject[] buttonsToEnable;
    public AudioSource clicksfx;

    private bool buttonsEnabled = false;

    void Awake()
    {
        ResetState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !buttonsEnabled)
        {
            EnableButtons();
        }
    }

    public void EnableButtons()
    {
        foreach (var button in buttonsToEnable)
        {
            button.SetActive(true);
        }
        pressToContinueText.gameObject.SetActive(false);

        buttonsEnabled = true;
        clicksfx.Play();
        enabled = false;
    }

    public void StartGame()
    {
        Debug.Log("Game started!");
    }

    void ResetState()
    {
        buttonsEnabled = false;

        pressToContinueText.gameObject.SetActive(true);

        foreach (var button in buttonsToEnable)
        {
            button.SetActive(false);
        }
    }
}
