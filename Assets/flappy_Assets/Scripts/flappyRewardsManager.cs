using TMPro;
using UnityEngine;

public class flappyRewardsManager : MonoBehaviour
{
    public static flappyRewardsManager instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI statusText;

    public bool winScreenActive = false;
    public bool loseScreenActive = false;

    private AugmentManager augmentManager;
    private StatusManager statusManager;
    private flappyScoreManager _flappyScoreManager;

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

        _flappyScoreManager = FindObjectOfType<flappyScoreManager>();
        if (_flappyScoreManager == null)
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
            flappyScoreManager.instance.obtainedScoreText.gameObject.SetActive(true);
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
                GetFlappyBuff();
                _flappyScoreManager.BaseScoring();
                return "Insurance Augment in effect! : No punishments received!\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                GetFlappyBuff();
                _flappyScoreManager.BaseScoring();
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
                _flappyScoreManager.BaseScoring();
                return "Multiplying Augment is active. : Augment conditions is not triggered.\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                GetFlappyBuff();
                _flappyScoreManager.MultiplierEffect();
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
            _flappyScoreManager.BaseScoring();
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
                GetFlappyBuff();
                _flappyScoreManager.BaseScoring();
                return "Augmentless : No punishments triggered!\n";
            }
            else if (loseScreenActive && !winScreenActive)
            {
                _flappyScoreManager.BaseScoring();
                return "Augmentless : Punishments triggered!\n";
            }
        }
        return "";
    }

    public void GetFlappyBuff()
    {
        if (statusManager != null)
        {
            //enter buff mechanic here
        }
        else
        {
            Debug.LogError("shopNullifier instance is null.");
        }
    }
}
