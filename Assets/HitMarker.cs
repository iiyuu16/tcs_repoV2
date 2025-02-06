using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    // Start is called before the first frame update
    int timesHit = 0;
    public GameObject turret;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timesHit > 20)
        {
            timesHit = 0;
            turret.gameObject.SetActive(false);
            Debug.Log("TURRET DEACTIVATED");
            StartCoroutine(respawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player Bullet")
        {
            timesHit++;
            Debug.Log("I GOT HITTTTT");
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(20f);
        turret.gameObject.SetActive(true);
        Debug.Log("turret respawns");
    }
}
