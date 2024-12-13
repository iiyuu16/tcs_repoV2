using UnityEngine;
using TMPro;
using System.Collections;

public class survivalConditionManager : MonoBehaviour
{
    public survivalPlayerMovement playerMovement;
    public GameObject loseScreen;
    public GameObject[] objectsToDisable;

    public void Update()
    {
        if (playerMovement.currHP < 1)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        loseScreen.SetActive(true);
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