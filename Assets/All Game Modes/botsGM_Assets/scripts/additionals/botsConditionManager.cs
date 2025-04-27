using UnityEngine;
using TMPro;

public class botsConditionManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject[] objectsToDisable;
    public GameObject[] targetObjs;
    public TextMeshProUGUI objRemaining;

    private bool hasWon = false;

    void Update()
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
        foreach (GameObject target in targetObjs)
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
        if (objRemaining != null)
        {
            int totalTargets = targetObjs.Length;
            int missingTargets = 0;

            foreach (GameObject target in targetObjs)
            {
                if (target == null)
                {
                    missingTargets++;
                }
            }

            objRemaining.text = $"Targets: {missingTargets}/{totalTargets}";
        }
    }
}