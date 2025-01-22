using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registerHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player Bullet")
        {
            this.gameObject.SetActive(false);
        }
    }
}
