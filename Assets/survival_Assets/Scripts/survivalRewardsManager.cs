using TMPro;
using UnityEngine;

public class survivalRewardsManager : MonoBehaviour
{
    public static survivalRewardsManager instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI statusText;

    public bool winScreenActive = false;
    public bool loseScreenActive = false;

    private AugmentManager augmentManager;
    private StatusManager statusManager;
    private survivalScoreManager _survivalScoreManager;

    public survivalMultiScore[] multiScoreEffects;

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

        _survivalScoreManager = FindObjectOfType<survivalScoreManager>();
        if (_survivalScoreManager == null)
        {
            Debug.Log("survivalScoreManager instance is not found in the scene.");
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
            survivalScoreManager.instance.obtainedScoreText.gameObject.SetActive(true);
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

            /*            if (augmentManager.isMultiplyingOnEffect && winScreenActive)
                        {
                            _sdScoreManager.MultiplierEffect();
                        }*/

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
                GetSurvivalBuff();
                _survivalScoreManager.BaseScoring();
                return "Insurance Augment in effect! : No punishments received!\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                GetSurvivalBuff();
                _survivalScoreManager.BaseScoring();
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
                _survivalScoreManager.BaseScoring();
                GetSurvivalDebuff();
                return "Multiplying Augment is active. : Augment conditions is not triggered.\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                if (multiScoreEffects != null)
                {
                    foreach (var multiScore in multiScoreEffects)
                    {
                        if (multiScore != null)
                        {
                            multiScore.enabled = true;
                        }
                    }
                }
                GetSurvivalBuff();
                _survivalScoreManager.MultiplierEffect();
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
            _survivalScoreManager.BaseScoring();
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
                GetSurvivalBuff();
                _survivalScoreManager.BaseScoring();
                return "Augmentless : No punishments triggered!\n";
            }
            else if (loseScreenActive && !winScreenActive)
            {
                GetSurvivalDebuff();
                _survivalScoreManager.BaseScoring();
                return "Augmentless : Punishments triggered!\n";
            }
        }
        return "";
    }

    public void GetSurvivalDebuff()
    {
        if (statusManager != null)
        {
            statusManager.shopDebuffOn();
            //at visnovMain, shop should be not accessible anymore
        }
        else
        {
            Debug.LogError("shopNullifier instance is null.");
        }
    }

    public void GetSurvivalBuff()
    {
        if (statusManager != null)
        {
            statusManager.shopBuffOn();
            //enter buff mechanic here
        }
        else
        {
            Debug.LogError("shopNullifier instance is null.");
        }
    }
}
