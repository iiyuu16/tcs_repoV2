using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AutoDestroyPoolableObject
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
        Debug.Log("I HIT SOMETHING");
        this.gameObject.SetActive(false);
        
    }
}
