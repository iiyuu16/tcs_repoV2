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
            int remainingTargets = CountActiveTargets();
            objRemaining.text = $"Bots: {remainingTargets}/5";

            if (remainingTargets == 0)
            {
                hasWon = true;
                WinGame();
            }
        }
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
        DisableGameObjects();
    }

    private void DisableGameObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    private int CountActiveTargets()
    {
        int activeTargets = 0;
        foreach (GameObject target in targetObjs)
        {
            if (target.activeSelf)
            {
                activeTargets++;
            }
        }
        return activeTargets;
    }

    private void Start()
    {
        objRemaining.text = $"Bots: {CountActiveTargets()}/5";
    }
}