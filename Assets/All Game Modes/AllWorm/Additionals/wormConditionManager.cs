using TMPro;
using UnityEngine;
using System.Collections;

public class wormConditionManager : MonoBehaviour
{
    public GameObject[] targetGameObjects;
    public GameObject winScreen;
    public GameObject[] objectsToDisable;
    public GameObject objectToEnable;
    public TextMeshProUGUI statusText;
    public wormTimeManager timeManager;
    private bool hasWon = false;

    private void Start()
    {
        if (timeManager == null)
        {
            Debug.LogError("wormTimeManager reference is missing.");
            return;
        }

    }

    private void Update()
    {
        if (!hasWon)
        {
            UpdateStatusText();

            if (AreAllTargetsDestroyed())
            {
                if (winScreen != null)
                {
                    winScreen.SetActive(true);
                }

                DisableGameObjects();
                hasWon = true;
            }
        }
    }

    private bool AreAllTargetsDestroyed()
    {
        foreach (GameObject target in targetGameObjects)
        {
            if (target != null)
            {
                return false;
            }
        }
        return true;
    }

    private void DisableGameObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
        objectToEnable.SetActive(true);
    }

    private void UpdateStatusText()
    {
        if (statusText != null)
        {
            int totalTargets = targetGameObjects.Length;
            int missingTargets = 0;

            foreach (GameObject target in targetGameObjects)
            {
                if (target == null)
                {
                    missingTargets++;
                }
                statusText.text = $"Cores: {missingTargets}/{totalTargets}";
            }
        }
    }
}