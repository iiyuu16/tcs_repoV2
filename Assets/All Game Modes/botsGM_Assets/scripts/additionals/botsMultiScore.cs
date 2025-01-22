using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botsMultiScore : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("on enable is triggered");

        if (botsScoreManager.instance != null)
        {
            botsScoreManager.instance.MultiplierEffect();
        }
        else
        {
            Debug.LogError("botsScoreManager instance is null.");
        }
    }
}
