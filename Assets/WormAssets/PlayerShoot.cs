using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //public GameObject turret;
    public Rigidbody rigid;
    public GameObject bullets;
    public float playerProjectileSpeed = 1.0f;
    private int fireProjectile = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = bullets.GetComponent<Rigidbody>();
        
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            //Debug.Log("Firing");
            Rigidbody p = Instantiate(rigid, transform.position, transform.rotation);
            p.velocity = transform.forward * playerProjectileSpeed;
            //OnEnable();

        }
    }

}
