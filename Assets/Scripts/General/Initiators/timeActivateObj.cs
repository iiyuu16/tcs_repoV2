using UnityEngine;
using TMPro;
using System.Collections;

public class TimeActivateObj : MonoBehaviour
{
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI goText;

    private float countdownTimer;
    private bool isCounting = true;

    private void Start()
    {
        countdownTimer = countdownTime;
        UpdateTimerText();

        StartCoroutine(CountdownCoroutine());
    }

    private void Update()
    {
        if (isCounting)
        {
            countdownTimer -= Time.deltaTime;
            UpdateTimerText();
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (countdownTimer > 0f)
        {
            yield return null;
        }

        timerText.gameObject.SetActive(false);
        goText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        isCounting = false;
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(countdownTimer);
        timerText.text = seconds.ToString();

        if (seconds == 0)
        {
            goText.text = "Go!";
        }
    }
}
