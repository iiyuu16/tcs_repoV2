using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    Rigidbody rb;
    public GameObject camera;
    
    private void Start()
    {
        
        //rb = bullet.GetComponent<Rigidbody>();
        //rb.AddForce(camera.transform.forward * 5f);
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = ObjectPoolRevamp.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
            }
            rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 20f;
            //StartCoroutine(fire());
           
        }
        
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(1f);
        
        
        Debug.Log("Firing");
        
    }
}
