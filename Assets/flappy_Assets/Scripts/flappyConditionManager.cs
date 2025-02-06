using UnityEngine;

public class flappyConditionManager : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject loseScreen;
    public GameObject[] objectsToDisable;

    void Update()
    {
        if (playerObj == null)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }

        DisableGameObjects();
        Debug.Log("game over");
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
}