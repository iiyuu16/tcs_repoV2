using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regHitBoss : MonoBehaviour
{
    public int HP = 25;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player Bullet")
        {
            HP--;
            Debug.Log(HP);
            if (HP < 0)
            {
                this.gameObject.SetActive(false);
            }
            
        }
    }
}
