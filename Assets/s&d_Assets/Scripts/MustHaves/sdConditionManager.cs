using UnityEngine;
using TMPro;

public class SdWinConditionManager : MonoBehaviour
{
    public GameObject[] targetGameObjects;
    public GameObject winScreen;
    public GameObject[] objectsToDisable;
    public TextMeshProUGUI statusText; // Add this line to reference your TMP component

    private bool hasWon = false;

    private void Update()
    {
        if (!hasWon)
        {
            // Update the status text
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
            }

            statusText.text = $"Targets Destroyed: {missingTargets}/{totalTargets}";
        }
    }
}
