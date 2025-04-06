using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class survivalPlayerMovement : MonoBehaviour
{
    public static survivalPlayerMovement instance;
    public float moveSpeed = 5f;
    public float rotationSpeed = 150f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    private float currentSpeed = 0f;
    public float maxInfection;
    public float currInfection;
    public float infectionRate = 0.1f;

    private bool isBraking = false;

    public survivalSoundSource sfx;

    public Slider sliderInf;
    public Image fx1;
    public Image fx2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currInfection = 0f;
        Debug.Log("starting hp:" + currInfection);

        if (sliderInf != null)
        {
            sliderInf.minValue = 0;
            sliderInf.maxValue = maxInfection;
            sliderInf.value = currInfection;
            sliderInf.onValueChanged.AddListener(OnHealthChanged);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyBrake();
        ApplyInfection();
    }

    public void OnHealthChanged(float value)
    {
        currInfection = Mathf.RoundToInt(value);
        fx1.color = new Color(fx1.color.r, fx1.color.g, fx1.color.b, currInfection / maxInfection);
        fx2.color = new Color(fx2.color.r, fx2.color.g, fx2.color.b, currInfection / maxInfection);
    }

    public void PlayerHit()
    {
        currInfection += 2;
        sfx.hurtSFX();
        Debug.Log("hp:" + currInfection);
        sdCamShake.instance.ShakeCamera();

        if (sliderInf != null)
        {
            sliderInf.value = currInfection;
        }
    }

    public void NegateInf(int amount)
    {
        currInfection -= amount;
        currInfection = Mathf.Max(currInfection, 0);
        Debug.Log("Infection: " + currInfection);
        if (sliderInf != null)
        {
            sliderInf.value = currInfection;
        }
    }

    private void ApplyInfection()
    {
        if (currInfection < 100)
        {
            currInfection += infectionRate * Time.deltaTime;
            sliderInf.value = currInfection;
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isBraking)
            {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Colliders"))
        {
            currentSpeed = 0f;
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
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
}