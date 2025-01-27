using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class plyrWorm : MonoBehaviour
{
    public static plyrWorm instance;
    public wormStunFX stunEffects;
    public Rigidbody rb;
    public GameObject plyrObj;
    public GameObject volumeFX;

    public float moveSpeed = 5f;
    public float rotationSpeed = 0.5f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    public float recoveryTime = 5f;
    private float currentSpeed = 0f;
    public float maxSpeed = 15f;
    public int maxHP;
    private int currHP;

    private bool isBraking = false;
    public bool isStunned = false;
    private Coroutine recoveryCoroutine;

    public Slider sliderHP;
    public survivalSoundSource sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currHP = maxHP;
        stunEffects.DisableStunEffects();
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
        if (!isStunned)
        {
            HandleMovement();
            HandleRotation();
            ApplyBrake();
        }
        HandleLife();
    }

    private void HandleLife()
    {
        if (currHP <= 0 && !isStunned)
        {
            isStunned = true;
            Debug.Log("Stunned state activated");
            sfx.stunSFX();
            currentSpeed = 0f;

            recoveryCoroutine = StartCoroutine(RecoveryState());
        }
    }

    private IEnumerator NormalScreen()
    {
        yield return new WaitForSeconds(1f);
       volumeFX.SetActive(false);
    }

    private IEnumerator RecoveryState()
    {
        GetComponent<Collider>().enabled = false;
        stunEffects.EnableStunEffects();
        stunEffects.ShowRecoveryTime(recoveryTime);

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
        stunEffects.DisableStunEffects();
        sfx.stopStunSFX();
        sfx.recoverSFX();
        isStunned = false;
        Debug.Log("Player recovered");
        currHP = maxHP;
        sliderHP.value = maxHP;
    }

    public void OnHealthChanged(float value)
    {
        currHP = Mathf.RoundToInt(value);
    }

    public void PlayerHit()
    {
        currHP -= 1;
        sfx.explosionSFX();
        Debug.Log("hp:" + currHP);
        volumeFX.SetActive(true);
        if (sliderHP != null)
        {
            sliderHP.value = currHP;
        }
        StartCoroutine(NormalScreen());
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isBraking)
            {
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
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
            rb.AddTorque(-plyrObj.transform.up * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(plyrObj.transform.up * rotationSpeed);
        }
    }

    private void ApplyBrake()
    {
        if (isBraking)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            rb.AddForce(-plyrObj.transform.up * deceleration);
        }
    }
}
