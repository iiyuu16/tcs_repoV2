using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BotDamage : MonoBehaviour
{
    public GameObject[] ToBeDisabled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            foreach (GameObject obj in ToBeDisabled)
            {
                obj.SetActive(false);
            }
            Debug.Log("player hit");
        }
    }
}
