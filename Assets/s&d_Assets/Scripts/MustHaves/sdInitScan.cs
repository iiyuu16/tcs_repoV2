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

    public float cooldownDuration = 0.5f;

    private SphereCollider sphereCollider;
    private bool isExpanding = false;
    private Material originalMaterial;
    private sdPlayerMovement playerMovement;

    private Coroutine revertMaterialCoroutine;

    public KeyCode Scan;
    public sdSoundSource sfx;

    private float nextScanTime;

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