using UnityEngine;

public class EnableObjsOnAwake: MonoBehaviour
{
    public GameObject[] objsToEnable;

    private void OnEnable()
    {
        foreach (GameObject obj in objsToEnable)
        {
            obj.SetActive(true);
        }
    }
}
