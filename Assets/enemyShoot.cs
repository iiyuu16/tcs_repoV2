using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("SAW PLAYER FIRING");
            StartCoroutine(fire());
        }
    }

    private IEnumerator fire()
    {
        while (true)
        {
            Debug.Log("WE'RE FIRING");
            GameObject bullet = enemyPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
            }
            rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = (transform.forward * 20f);
            yield return new WaitForSeconds(0.25f);
        }
        
    }
}
