using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flappyGroundMovement : MonoBehaviour
{
    public float life = 20f;

    void Update()
    {
        life -= Time.deltaTime;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(0,0, -1*Time.deltaTime);
        }
    }
}
