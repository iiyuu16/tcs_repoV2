using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretEnabler : MonoBehaviour
{
    public wormEnemyMovement worm;
    private int fireNext = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<turretAim>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(worm.seen == 1 && fireNext == 1)
        {
            
            StartCoroutine(rateOfFire());
            //fireNext = 0;
            
        }
        else
        {
            GetComponent<turretAim>().enabled = false;
            worm.seen = 0;
        }
    }

    IEnumerator rateOfFire()
    {
        while (worm.seen == 1)
        {
            Debug.Log("ENABLEDFIRING");
            //GetComponent<turretAim>().enabled = true;
            
            yield return new WaitForSeconds(30);
            fireNext = 0;
            //gameObject.GetComponent<turretAim>().enabled = false;
            //fireNext = 1;
        }

    }
}
