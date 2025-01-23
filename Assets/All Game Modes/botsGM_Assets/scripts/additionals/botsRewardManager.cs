using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class botsRewardManager : MonoBehaviour
{
    public static botsRewardManager instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI statusText;

    public bool winScreenActive = false;
    public bool loseScreenActive = false;

    private AugmentManager augmentManager;
    private StatusManager statusManager;
    private botsScoreManager _botsScoreManager;

    private bool scoreTriggered = false;

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

        augmentManager = FindObjectOfType<AugmentManager>();
        if (augmentManager == null)
        {
            Debug.Log("AugmentManager instance is not found in the scene.");
            return;
        }

        statusManager = FindObjectOfType<StatusManager>();
        if (statusManager == null)
        {
            Debug.Log("StatusManager instance is not found in the scene.");
            return;
        }

        _botsScoreManager = FindObjectOfType<botsScoreManager>();
        if (_botsScoreManager == null)
        {
            Debug.Log("botsScoreManager instance is not found in the scene.");
            return;
        }
    }

    void Update()
    {
        DetermineActiveScreen();
    }

    public void DetermineActiveScreen()
    {
        winScreenActive = winScreen.activeInHierarchy;
        loseScreenActive = loseScreen.activeInHierarchy;

        if (winScreenActive || loseScreenActive)
        {
            statusText.gameObject.SetActive(true);
            ApplyAugmentEffects();
            botsScoreManager.instance.obtainedScoreText.gameObject.SetActive(true);
        }
        else
        {
            statusText.gameObject.SetActive(false);
        }
    }

    private void ApplyAugmentEffects()
    {
        if (!scoreTriggered)
        {
            string effectMessage = "";

            effectMessage = ApplyInsuranceEffect();
            if (string.IsNullOrEmpty(effectMessage)) effectMessage = ApplyMultiplyingEffect();
            if (string.IsNullOrEmpty(effectMessage)) effectMessage = ApplyHollowingEffect();
            if (string.IsNullOrEmpty(effectMessage)) effectMessage = defaultEffects();

            statusText.text = effectMessage.Trim();
            statusText.gameObject.SetActive(true);

            scoreTriggered = true;
        }
    }

    private string ApplyInsuranceEffect()
    {
        if (augmentManager.isInsuranceActive)
        {
            augmentManager.isInsuranceOnEffect = true;
            if (loseScreenActive && !winScreenActive)
            {
                GetBotsBuff();
                _botsScoreManager.BaseScoring();
                return "Insurance Augment in effect! : No punishments received!\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                GetBotsBuff();
                _botsScoreManager.BaseScoring();
                return "Insurance Augment is active! : Augment skill is not triggered.\n";
            }
        }
        return "";
    }

    private string ApplyMultiplyingEffect()
    {
        if (augmentManager.isMultiplyingActive)
        {
            augmentManager.isMultiplyingOnEffect = true;
            if (loseScreenActive && !winScreenActive)
            {
                GetBotsDebuff();
                _botsScoreManager.BaseScoring();
                return "Multiplying Augment is active. : Augment conditions is not triggered.\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                GetBotsBuff();
                _botsScoreManager.MultiplierEffect();
                return "Multiplying Augment in effect! : Obtained additional Fragments!\n";
            }
        }
        return "";
    }

    private string ApplyHollowingEffect()
    {
        if (augmentManager.isHollowingActive)
        {
            augmentManager.isHollowingOnEffect = true;
            _botsScoreManager.BaseScoring();
            return "Hollowing Augment in effect! : No buffs or debuffs granted!\n";
        }
        return "";
    }

    private string defaultEffects()
    {
        if (augmentManager.isAugmentless)
        {
            if (winScreenActive && !loseScreenActive)
            {
                GetBotsDebuff();
                _botsScoreManager.BaseScoring();
                return "Augmentless : No punishments triggered!\n";
            }
            else if (loseScreenActive && !winScreenActive)
            {
                GetBotsDebuff();
                _botsScoreManager.BaseScoring();
                return "Augmentless : Punishments triggered!\n";
            }
        }
        return "";
    }

    public void GetBotsBuff()
    {
        if (statusManager != null)
        {
            statusManager.setToDefaultStatus();
        }
        else
        {
            Debug.LogError("no botsDebuff");
        }
        statusManager.glitchDebuffOff();
    }

    public void GetBotsDebuff()
    {
        if (statusManager != null)
        {
            statusManager.glitchDebuffOn();
        }
        else
        {
            Debug.LogError("no botsDebuff");
        }
    }
}
