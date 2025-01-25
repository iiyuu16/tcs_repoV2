using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormSpawner : MonoBehaviour
{
    public GameObject enemyToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyToEnable.SetActive(true);
            Debug.Log("Worm spawned");
        }
    }
}
