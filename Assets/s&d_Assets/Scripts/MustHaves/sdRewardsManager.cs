using UnityEngine;
using TMPro;

public class sdRewardsManager : MonoBehaviour
{
    public static sdRewardsManager instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI statusText;

    public bool winScreenActive = false;
    public bool loseScreenActive = false;

    private AugmentManager augmentManager;
    private StatusManager statusManager;
    private sdScoreManager _sdScoreManager;

    public sdMultiScore[] multiScoreEffects;

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

        _sdScoreManager = FindObjectOfType<sdScoreManager>();
        if (_sdScoreManager == null)
        {
            Debug.Log("sdScoreManager instance is not found in the scene.");
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
            sdScoreManager.instance.obtainedScoreText.gameObject.SetActive(true);
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
                GetSDBuff();
                _sdScoreManager.BaseScoring();
                return "Insurance Augment in effect! : No punishments received!\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                GetSDBuff();
                _sdScoreManager.BaseScoring();
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
                _sdScoreManager.BaseScoring();
                GetSDDebuff();
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
                GetSDBuff();
                _sdScoreManager.MultiplierEffect();
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
            _sdScoreManager.BaseScoring();
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
                GetSDBuff();
                _sdScoreManager.BaseScoring();
                return "Augmentless : No punishments triggered!\n";
            }
            else if (loseScreenActive && !winScreenActive)
            {
                GetSDDebuff();
                _sdScoreManager.BaseScoring();
                return "Augmentless : Punishments triggered!\n";
            }
        }
        return "";
    }

    public void GetSDDebuff()
    {
        if (statusManager != null)
        {
            statusManager.shopDebuffOn();
            Debug.Log("shop inflation is now enabled");
        }
        else
        {
            Debug.LogError("PopUpManager instance is null.");
        }
    }

    public void GetSDBuff()
    {
        if (statusManager != null)
        {
            statusManager.shopBuffOn();

            Debug.Log("shop discount is now enabled");
        }
        else
        {
            Debug.LogError("PopUpManager instance is null.");
        }
    }

}
