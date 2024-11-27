using UnityEngine;
using TMPro;

public class survivalTimer : MonoBehaviour
{
    public float timeValue = 60;
    public TextMeshProUGUI timeText;
    public GameObject winScreen;
    public GameObject[] objectsToDisable;

    private void Start()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
        {
            timeValue = 0;
            winTrigger();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay - Mathf.FloorToInt(timeToDisplay)) * 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void winTrigger()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }

        DisableGameObjects();
    }

    private void DisableGameObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
