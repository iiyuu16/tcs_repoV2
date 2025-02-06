using UnityEngine;

public class survivalMultiScore : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("on enable is triggered");

        if (survivalScoreManager.instance != null)
        {
            survivalScoreManager.instance.MultiplierEffect();
        }
        else
        {
            Debug.LogError("survivalScoreManager instance is null.");
        }
    }
}
