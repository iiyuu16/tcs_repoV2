using UnityEngine;

public class mazeConditionManager : MonoBehaviour
{
    public CursorPlayer mazePlayer;
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    public GameObject winScreen;

    public void mazeWin()
    {
        winScreen.SetActive(true);

        foreach (var obj in objectsToEnable)
            if (obj != null) obj.SetActive(true);

        foreach (var obj in objectsToDisable)
            if (obj != null) obj.SetActive(false);
    }
}