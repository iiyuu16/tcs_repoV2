using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class WormMovementRevamp : MonoBehaviour
{
    Rigidbody rb;
    public float initThrust = 10f;
    bool isPushing = false;
    bool isTurningLeft = false;
    bool isTurningRight = false;
    bool isBraking = false;
    public GameObject cam;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        rb = sphere.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isPushing = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isTurningLeft = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isTurningRight = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isBraking = true;
        }
    }

    private void FixedUpdate()
    {
        if (isPushing == true)
        {
            Debug.Log("Pushing");
            rb.AddForce(cam.transform.forward * initThrust);
            isPushing=false;
        }
        if (isBraking == true)
        {
            Debug.Log("Pushing");
            rb.AddForce(-cam.transform.forward * initThrust);
            isBraking = false;
        }
        if (isTurningLeft == true)
        {
            Debug.Log("Pushing");
            rb.AddTorque(-cam.transform.up * 1f);
            isTurningLeft = false;
        }
        if (isTurningRight == true)
        {
            Debug.Log("Pushing");
            rb.AddTorque(cam.transform.up * 1f);
            isTurningRight = false;
        }
    }
}
