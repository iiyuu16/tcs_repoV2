using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections;

public class wormStunFX : MonoBehaviour
{
    public static wormStunFX instance;

    public GameObject stunUI;
    public GameObject volumeFX;
    public TextMeshProUGUI countdownText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void EnableStunEffects()
    {
        if (stunUI != null)
        {
            stunUI.SetActive(true);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        volumeFX.SetActive(true);
    }

    public void DisableStunEffects()
    {
        if (stunUI != null)
        {
            stunUI.SetActive(false);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        volumeFX.SetActive(false);
    }

    public void ShowRecoveryTime(float recoveryTime)
    {
        if (countdownText != null)
        {
            StartCoroutine(UpdateRecoveryTime(recoveryTime));
        }
    }

    private IEnumerator UpdateRecoveryTime(float recoveryTime)
    {
        float timer = recoveryTime;

        while (timer > 0)
        {
            countdownText.text = "==" + Mathf.CeilToInt(timer) + "==";
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        countdownText.text = "";
    }
}
