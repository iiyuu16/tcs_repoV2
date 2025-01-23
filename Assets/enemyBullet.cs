using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : AutoDestroyPoolableObject
{
    [HideInInspector]
    public Rigidbody rigidbody;
    public Vector3 speed = new Vector3(200, 0);

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        rigidbody.velocity = speed;

    }

    //public override void OnDisable()
    //{
    //base.OnDisable();
    //rigidbody.velocity = Vector3.zero;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
        //this.gameObject.SetActive(false);

    }
}
