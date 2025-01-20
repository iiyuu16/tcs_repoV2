using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BotDamage : MonoBehaviour
{
    public GameObject ship;
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
        if (other.gameObject.tag == "Enemy")
        {
            ship.gameObject.SetActive(false);
        }
    }
}
