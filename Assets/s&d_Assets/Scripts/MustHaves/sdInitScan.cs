using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class sdInitScan : MonoBehaviour
{
    public static sdInitScan instance;
    public event Action<float> OnRevealDurationOver;
    public ParticleSystem scanner;
    public float scanDuration = 3f;
    public float revealDuration = 5f;
    public float maxSize = 30;
    public float growthRate = 1f;
    public float startingSize = 0.1f;
    public Material newMaterial;
    public GameObject scanText;
    public float cooldownDuration = 3f;

    private SphereCollider sphereCollider;
    private bool isExpanding = false;
    private Material originalMaterial;
    private sdPlayerMovement playerMovement;

    private Coroutine revertMaterialCoroutine;

    public KeyCode Scan;
    public sdSoundSource sfx;

    private float nextScanTime;
    private bool showScanInd = false;

    void Start()
    {
        instance = this;
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = startingSize;

        Renderer targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalMaterial = targetRenderer.material;
        }
        else
        {
            Debug.LogWarning("Target does not have a Renderer component.");
        }

        playerMovement = FindObjectOfType<sdPlayerMovement>();
    }

    void Update()
    {
        if (!playerMovement.isStunned && Input.GetKeyDown(Scan) && Time.time >= nextScanTime)
        {
            scanner.Play();
            sfx.scanSFX();
            StartExpanding();
            nextScanTime = Time.time + cooldownDuration;

            if (scanText != null)
            {
                scanText.SetActive(false);
            }
        }

        if(Time.time >= nextScanTime && scanText != null)
        {
            showScanInd = true;
            if (scanText != null)
            {
                scanText.SetActive(true);
                Animator anim = scanText.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.Play("scanText");
                }
            }
        }

        if(showScanInd && scanText != null)
        {
            StartCoroutine(DisableScanText());
        }


        if (isExpanding)
        {
            ExpandCollider();
        }
    }

    void StartExpanding()
    {
        isExpanding = true;
    }

    void ExpandCollider()
    {
        if (sphereCollider.radius < maxSize)
        {
            sphereCollider.radius += growthRate * Time.deltaTime;
        }
        else
        {
            sphereCollider.radius = startingSize;
            isExpanding = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("Target detected in range");

            Renderer targetRenderer = other.GetComponent<Renderer>();
            if (targetRenderer != null && newMaterial != null)
            {
                Material originalMaterial = targetRenderer.material;
                targetRenderer.material = newMaterial;

                if (revertMaterialCoroutine != null)
                {
                    StopCoroutine(revertMaterialCoroutine);
                }

                revertMaterialCoroutine = StartCoroutine(RevertMaterialAfterDelay(targetRenderer, originalMaterial));
            }
            else
            {
                Debug.LogWarning("Target or new material is not assigned.");
            }
        }
    }

    IEnumerator DisableScanText()
    {
        showScanInd = false;

        Animator anim = scanText.GetComponent<Animator>();
        if (anim != null)
        {
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        }

        if (scanText != null)
        {
            scanText.SetActive(false);
        }
    }

    IEnumerator RevertMaterialAfterDelay(Renderer targetRenderer, Material originalMaterial)
    {
        yield return new WaitForSeconds(revealDuration);

        if (targetRenderer != null)
        {
            targetRenderer.material = originalMaterial;
            OnRevealDurationOver?.Invoke(revealDuration);
        }
    }
}