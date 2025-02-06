using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flappyGroundSpawner : MonoBehaviour
{
    public float timer = 0f;
    public float freq = 1f;
    public GameObject lowObj;
    public GameObject highObj;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(lowObj, new Vector3(0f, 0f, 2.9f), transform.rotation);
            Instantiate(highObj, new Vector3(0f, 2f, 2.9f), transform.rotation);
            timer  = freq;
        }
    }
}
