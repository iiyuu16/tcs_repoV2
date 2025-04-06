using UnityEngine;
using System.Collections;

public class survivalHPItem : MonoBehaviour
{
    public int cleanInfection = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            survivalPlayerMovement player = other.GetComponent<survivalPlayerMovement>();
            if (player != null)
            {
                player.NegateInf(cleanInfection);
                Destroy(gameObject);
            }
        }
    }
}