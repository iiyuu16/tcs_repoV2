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
    public float maxHP;
    public float currHP;
    public float decayRate = 0.1f;

    private bool isBraking = false;

    public survivalSoundSource sfx;

    public Slider sliderHP;

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
        currHP = maxHP;
        Debug.Log("starting hp:" + currHP);

        if (sliderHP != null)
        {
            sliderHP.minValue = 0;
            sliderHP.maxValue = maxHP;
            sliderHP.value = maxHP;
            sliderHP.onValueChanged.AddListener(OnHealthChanged);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyBrake();
        ApplyHPDecay();
    }

    public void OnHealthChanged(float value)
    {
        currHP = Mathf.RoundToInt(value);
    }

    public void PlayerHit()
    {
        currHP -= 1;
        sfx.hurtSFX();
        Debug.Log("hp:" + currHP);
        sdCamShake.instance.ShakeCamera();

        if (sliderHP != null)
        {
            sliderHP.value = currHP;
        }
    }

    public void AddHealth(int amount)
    {
        currHP += amount;
        currHP = Mathf.Min(currHP, maxHP);
        Debug.Log("hp:" + currHP);
        if (sliderHP != null)
        {
            sliderHP.value = currHP;
        }
    }

    private void ApplyHPDecay()
    {
        if (currHP > 0)
        {
            currHP -= decayRate * Time.deltaTime;
            sliderHP.value = currHP;
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