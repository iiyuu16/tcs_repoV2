using UnityEngine;
using TMPro;

public class survivalTimer : MonoBehaviour
{
    public float timeValue = 180.5f;
    public TextMeshProUGUI timeText;
    public GameObject winScreen;
    public GameObject[] objectsToDisable;

    public GameObject firstWave; 
    public GameObject secondWave;
    private bool firstMinuteTriggered = false;
    private bool secondMinuteTriggered = false;

    private void Start()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }

        SetActiveForObject(firstWave, false);
        SetActiveForObject(secondWave, false);
    }

    private void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);

            if (!firstMinuteTriggered && timeValue <= 120.5f)
            {
                SetActiveForObject(firstWave, true);
                firstMinuteTriggered = true;
            }

            if (!secondMinuteTriggered && timeValue <= 60.5f)
            {
                SetActiveForObject(secondWave, true);
                secondMinuteTriggered = true;
            }
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
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void SetActiveForObject(GameObject obj, bool state)
    {
        if (obj != null)
        {
            obj.SetActive(state);
        }
    }
}
