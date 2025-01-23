using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class botsTimer : MonoBehaviour
{
    public float timeValue = 300.5f;
    public TextMeshProUGUI timeText;
    public GameObject lifeObj;
    public GameObject loseScreen;
    public GameObject[] objectsToDisable;

    private void Start()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
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
            loseTrigger();
        }

        if (lifeObj != null && !lifeObj.activeSelf)
        {
            loseTrigger();
        }
    }

    private void loseTrigger()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
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

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay - Mathf.FloorToInt(timeToDisplay)) * 1000;

        timeText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
