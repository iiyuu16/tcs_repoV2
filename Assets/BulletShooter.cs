using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    Rigidbody rb, sb;
    public GameObject camera;
    public GameObject ship;
    
    private void Start()
    {
        sb = ship.GetComponent<Rigidbody>();
        //rb = bullet.GetComponent<Rigidbody>();
        //rb.AddForce(camera.transform.forward * 5f);
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject PlayerBullet = ObjectPoolRevamp.SharedInstance.GetPooledObject();
            if (PlayerBullet != null)
            {
                PlayerBullet.transform.position = this.transform.position;
                PlayerBullet.transform.rotation = this.transform.rotation;
                PlayerBullet.SetActive(true);
            }
            rb = PlayerBullet.GetComponent<Rigidbody>();
            rb.velocity = (transform.forward * 20f); 
            //StartCoroutine(fire());
           
        }
        
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(1f);
        
        
        Debug.Log("Firing");
        
    }
}
