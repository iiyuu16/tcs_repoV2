using UnityEngine;
using TMPro;
using System.Collections;

public class SdWinConditionManager : MonoBehaviour
{
    public GameObject[] targetGameObjects;
    public GameObject winScreen;
    public GameObject[] objectsToDisable;
    public TextMeshProUGUI statusText;
    public sdTimer timeManager;
    public float timeToSwitch;

    private bool hasWon = false;
    private bool isAlternate = false;
    private Coroutine toggleTargetsCoroutine;

    private void Start()
    {
        if (timeManager == null)
        {
            Debug.LogError("TimeManager reference is missing.");
            return;
        }

        toggleTargetsCoroutine = StartCoroutine(ToggleTargets());
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

                if (toggleTargetsCoroutine != null)
                {
                    StopCoroutine(toggleTargetsCoroutine);
                }
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

    private IEnumerator ToggleTargets()
    {
        while (!hasWon)
        {
            yield return new WaitForSeconds(timeToSwitch);
            ToggleTargetsHalf(isAlternate);
            isAlternate = !isAlternate;
        }
    }

    private void ToggleTargetsHalf(bool firstHalf)
    {
        int midIndex = targetGameObjects.Length / 2;

        for (int i = 0; i < targetGameObjects.Length; i++)
        {
            if (targetGameObjects[i] != null)
            {
                bool shouldBeActive = firstHalf ? (i >= midIndex) : (i < midIndex);
                targetGameObjects[i].SetActive(shouldBeActive);
            }
        }
    }

}
