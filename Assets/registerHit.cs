using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registerHit : MonoBehaviour
{
    public int healthBar = 10;
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
        if (other.gameObject.tag == "Player Bullet")
        {
            healthBar--;
            if(healthBar == 0)
            {
                this.gameObject.SetActive(false);
            }
            
        }
    }
}
