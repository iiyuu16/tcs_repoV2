using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormDefeat : MonoBehaviour
{
    public GameObject ship;
    public int playerHP = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Debug.Log("WE GOT HIT");
            playerHP--;
            if(playerHP < 0)
            {
                ship.gameObject.SetActive(false);
            }
        }
    }
}
