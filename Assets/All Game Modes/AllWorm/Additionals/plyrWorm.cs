using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class plyrWorm : MonoBehaviour
{
    public static plyrWorm instance;
    public wormStunFX stunEffects;
    public Rigidbody rb;
    public GameObject plyrObj;
    public GameObject volumeFX;
    public GameObject transition;
    private bool isBraking = false;
    public bool isStunned = false;

    public float recoveryTime = 5f;
    public int maxHP;
    private int currHP;

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
        rb = plyrObj.GetComponent<Rigidbody>();

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
        HandleLife();
    }

    private void HandleLife()
    {
        if (currHP <= 0)
        {
            isStunned = true;
            Debug.Log("Stunned state activated");
            sfx.stunSFX();
            recoveryCoroutine = StartCoroutine(RecoveryState());
            transition.SetActive(true);
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

}
