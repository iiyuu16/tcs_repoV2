using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using Melanchall.DryWetMidi.Core;


public class WormMovementRevamp : MonoBehaviour
{
    Rigidbody rb,bb;
    public float initThrust = 0.001f;
    bool isPushing = false;
    bool isTurningLeft = false;
    bool isTurningRight = false;
    bool isBraking = false;
    public GameObject cam;
    public GameObject sphere;
    Vector2 left, right;
    public Transform head;
    public Transform target;
    public Transform origin;
    // Start is called before the first frame update
    void Start()
    {
        rb = sphere.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //left = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (Input.GetKey(KeyCode.W) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))

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
        if (Input.GetKey(KeyCode.S) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        { 
            isBraking = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            XROrigin xrOrigin = GetComponent<XROrigin>();
            xrOrigin.MoveCameraToWorldLocation(target.position);
            xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);
        }


    }

    private void FixedUpdate()
    {
        if (isPushing == true)
        {
            //Debug.Log("Pushing");
            rb.AddForce(cam.transform.forward * initThrust);
            isPushing=false;
        }
        if (isBraking == true)
        {
            //Debug.Log("Pushing");
            rb.AddForce(-cam.transform.forward * initThrust);
            isBraking = false;
        }
        if (isTurningLeft == true)
        {
            //Debug.Log("Pushing");
            rb.AddTorque(-cam.transform.up * 1f);
            isTurningLeft = false;
        }
        if (isTurningRight == true)
        {
            //Debug.Log("Pushing");
            rb.AddTorque(cam.transform.up * 1f);
            isTurningRight = false;
        }


    }
}
