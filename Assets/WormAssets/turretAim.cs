using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAim : MonoBehaviour
{
    //public GameObject turret;
    public wormEnemyMovement worm;
    public Transform target;
    public Rigidbody rb;
    public GameObject bullet;
    public float projectileSpeed = 1.0f;
    private int fireProjectile = 0;

    public AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = bullet.GetComponent<Rigidbody>();
        StartCoroutine(fire());
    }


    private void Update()
    {
        if (worm.seen == 1)
        {
            transform.LookAt(target);
            //Debug.Log("Firing");
            fireProjectile = 1;
            //OnEnable();

        }
        
    }

    private void OnEnable()
    {
        StartCoroutine(fire());

    }

    private void OnDisable()
    {
        //StopCoroutine(fire());
        Debug.Log("DISABLED GUNSSS");
    }

    IEnumerator fire()
    {
        while (true)
        {
            audio.Play();
            Debug.Log("FIREFIREFIRE");
            yield return new WaitForSeconds(0.5f);
            Rigidbody p = Instantiate(rb, transform.position, transform.rotation);
            p.velocity = transform.forward * projectileSpeed;
            //fireProjectile = 0;
            
        }
    }
}
