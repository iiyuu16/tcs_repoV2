using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class WormMovement : MonoBehaviour
{
    public static WormMovement instance;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float boostSpeed = 10f;
    public float boostCD = 3f;
    public float recoveryTime = 5f;
    public float boostRegenDelay = 10f;
    private float currentSpeed = 0f;
    public int maxHP;
    private int currHP;

     private bool isBraking = false;
    private bool isBoosting = false;
    public bool isStunned = false;
    private Coroutine recoveryCoroutine;

     public int maxBoosts;
    private int currBoosts;
    public Image[] boostIndicator;
    public Sprite boostIcon;
    public Sprite nullIcon;
    public Slider sliderHP;

    public KeyCode BoostKey;
    public sdSoundSource sfx;

    public AudioSource audio;

    internal Vector3 vPointPosition;
    internal Vector3 vPointDirection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currBoosts = maxBoosts;
        currHP = maxHP;
        UpdateBoostIndicator();
        StartCoroutine(BoostRegeneration());
        Debug.Log("starting hp:" + currHP);

        if (sliderHP != null)
        {
            sliderHP.minValue = 0;
            sliderHP.maxValue = maxHP;
            sliderHP.value = maxHP;
            sliderHP.onValueChanged.AddListener(OnHealthChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            HandleMovement();
            HandleRotation();
            ApplyBrake();
            HandleBoost();
        }
        HandleLife();
    }

    private void HandleLife()
    {
        if (currHP <= 0 && !isStunned)
        {
            //isStunned = true;
            Debug.Log("Stunned state activated");
            //sfx.stunSFX();
            //currentSpeed = 0f;

            //recoveryCoroutine = StartCoroutine(RecoveryState());
        }
    }

    private IEnumerator RecoveryState()
    {
        GetComponent<Collider>().enabled = false;
        //stunEffects.EnableStunEffects();
        //stunEffects.ShowRecoveryTime(recoveryTime);

        float timer = recoveryTime;
        while (timer > 0)
        {
            if (sliderHP != null)
            {
                sliderHP.value = Mathf.RoundToInt(timer);
            }
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        GetComponent<Collider>().enabled = true;
        //stunEffects.DisableStunEffects();
        //sfx.stopStunSFX();
        //sfx.recoverSFX();
        isStunned = false;
        Debug.Log("Player recovered");
        currHP = maxHP;
        UpdateBoostIndicator();
    }


    public void OnHealthChanged(float value)
    {
        currHP = Mathf.RoundToInt(value);
    }

    private void HandleMovement()
    {

        UnityEngine.XR.InputDevice handLDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        bool posLSupported = handLDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 posL);
        vPointPosition = transform.TransformPoint(posL);
        bool rotRSupported = handLDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotL);
        vPointDirection = rotL * Vector3.forward;
        vPointDirection = transform.TransformDirection(vPointDirection);
        //if (Input.GetKey(KeyCode.W))
        if (vPointDirection == transform.forward)
        {
            if (!isBraking)
            {
                //audio.Play();
                currentSpeed += acceleration * Time.deltaTime;

            }
            else
            {
                isBraking = false;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isBraking = true;
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Rotate(0f, -rotationSpeed * Time.deltaTime * currentSpeed, 0f);
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.Rotate(0f, rotationSpeed * Time.deltaTime * currentSpeed, 0f);
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }

    private void ApplyBrake()
    {
        if (isBraking)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, moveSpeed);
        }
    }

    private void HandleBoost()
    {
        if (Input.GetKeyDown(BoostKey) && !isBoosting && currBoosts > 0)
        {
            StartCoroutine(ActivateBoost());
        }
    }

    private IEnumerator ActivateBoost()
    {
        isBoosting = true;
        sfx.boostSFX();
        currentSpeed += boostSpeed;
        currBoosts--;
        UpdateBoostIndicator();
        //playerTrail.Play();

        yield return new WaitForSeconds(boostCD);

        isBoosting = false;
        //playerTrail.Stop();
        currentSpeed -= boostSpeed;
    }

    private IEnumerator BoostRegeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(boostRegenDelay);

            if (currBoosts < maxBoosts)
            {
                currBoosts++;
                UpdateBoostIndicator();
            }
        }
    }

    private void UpdateBoostIndicator()
    {
        for (int i = 0; i < boostIndicator.Length; i++)
        {
            if (i < currBoosts)
            {
                boostIndicator[i].sprite = boostIcon;
            }
            else
            {
                boostIndicator[i].sprite = nullIcon;
            }
        }
    }
}
