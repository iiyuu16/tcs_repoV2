using UnityEngine;
using TMPro;

public class RhythmRewardsManager : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    private rhythmScoreManager _rhythmScoreManager;
    private AugmentManager augmentManager;
    private StatusManager statusManager;
    public GameObject failTrigger;

    public GameObject winScreen;
    public GameObject loseScreen;
    private bool winScreenActive = false;
    private bool loseScreenActive = false;

    public rhythmMultiScore[] multiScoreEffects;

    private bool scoreTriggered = false;

    public void Start()
    {
        augmentManager = AugmentManager.instance;

        if (augmentManager == null)
        {
            Debug.LogError("AugmentManager instance not found in the scene.");
            return;
        }

        _rhythmScoreManager = rhythmScoreManager.instance;

        if (_rhythmScoreManager == null)
        {
            Debug.LogError("RhythmScoreManager instance not found in the scene.");
            return;
        }

        statusManager = StatusManager.instance;
        if (statusManager == null)
        {
            Debug.LogError("StatusManager instance not found in the scene.");
            return;
        }

        statusText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (rhythmSongManager.Instance.isSongFinished)
        {
            DetermineActiveScreen();
            ApplyAugmentEffects();
        }
    }

    public void DetermineActiveScreen()
    {
        if (failTrigger.activeInHierarchy)
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(true);
        }
        else if (!failTrigger.activeInHierarchy)
        {
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
        }

        winScreenActive = winScreen.activeInHierarchy;
        loseScreenActive = loseScreen.activeInHierarchy;
        statusText.gameObject.SetActive(true);
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
                _rhythmScoreManager.MultiplierEffect();
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
                statusManager.popupDebuffOff();
                _rhythmScoreManager.BaseScoring();
                return "Insurance Augment in effect! : No punishments received!\n";
            }
            else if (winScreenActive && !loseScreenActive)
            {
                statusManager.popupDebuffOff();
                _rhythmScoreManager.BaseScoring();
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
                _rhythmScoreManager.BaseScoring();
                GetRhythmDebuff();
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
                statusManager.popupDebuffOff();
                _rhythmScoreManager.MultiplierEffect();
                return "Multiplying Augment in effect! : Obtained additional Fragments!\n";
            }
        }
        return "";
    }

    private string ApplyHollowingEffect()
    {
        if (augmentManager.isHollowingActive)
        {
            statusManager.popupDebuffOff();
            augmentManager.isHollowingOnEffect = true;
            _rhythmScoreManager.BaseScoring();
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
                statusManager.popupDebuffOff();
                _rhythmScoreManager.BaseScoring();
                return "Augmentless : No punishments triggered!\n";
            }
            else if (loseScreenActive && !winScreenActive)
            {
                GetRhythmDebuff();
                _rhythmScoreManager.BaseScoring();
                return "Augmentless : Punishments triggered!\n";
            }
        }
        
        return "";
    }

    public void GetRhythmDebuff()
    {
        if (statusManager != null)
        {
            statusManager.popupDebuffOn();
            Debug.Log("pop-ups are now enabled");
        }
        else
        {
            Debug.LogError("PopUpManager instance is null.");
        }
    }
}