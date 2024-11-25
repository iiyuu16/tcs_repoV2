using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    int collected = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            //Debug.Log("PLAYER DETECTED");
            collected = other.GetComponent<PlayerCollect>().collected;
            if(collected == 0)
            {
                this.gameObject.SetActive(false);
                collected = 1;
            }
            
        }
        
    }
}
