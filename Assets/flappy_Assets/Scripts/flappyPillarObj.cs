using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flappyPillarObj : MonoBehaviour
{
    public float timer = 1;
    public float freq = 2;
    public float pos;
    public GameObject pillarObj;
    public GameObject playerObj;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            pos = Random.Range(.6f, 1.3f);
            transform.position = new Vector3(0, pos, 3f);
            Instantiate(pillarObj, transform.position, transform.rotation);
            timer = freq;
        }

        if(playerObj != null)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
