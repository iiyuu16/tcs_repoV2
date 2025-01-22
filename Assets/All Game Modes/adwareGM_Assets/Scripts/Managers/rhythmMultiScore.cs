using UnityEngine;

public class rhythmMultiScore : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("on enable is triggered");

        if (rhythmScoreManager.instance != null)
        {
            rhythmScoreManager.instance.MultiplierEffect();
        }
        else
        {
            Debug.LogError("rhythmScoreManager instance is null.");
        }
    }
}
