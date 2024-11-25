using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AugmentManager : MonoBehaviour
{
    public static AugmentManager instance;
    public TextMeshProUGUI[] augStatusTexts;

    [Header("Insurance Augment")]
    public bool isInsuranceBought;
    public bool isInsuranceActive;
    public bool isInsuranceOnEffect;

    [Header("Multiplying Augment")]
    public bool isMultiplyingBought;
    public bool isMultiplyingActive;
    public bool isMultiplyingOnEffect;

    [Header("Hollowing Augment")]
    public bool isHollowingBought;
    public bool isHollowingActive;
    public bool isHollowingOnEffect;

    [Header("Augmentless")]
    public bool isAugmentless = true;

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

        if (SceneManager.GetActiveScene().name == "VisNov_Prologue")
        {
            ResetAugments();

            LoadAugments();
        }
        else
        {
            LoadAugments();
        }
    }


    public void DisplayCurrentAugments()
    {
        if (augStatusTexts == null || augStatusTexts.Length == 0)
        {
            Debug.Log("AugmentStatusText array is null or empty. Cannot update text.");
            return;
        }

        string currentStatus = "";

        if (isInsuranceActive)
        {
            currentStatus += "Insurance Augment: Active\n";
        }
        if (isMultiplyingActive)
        {
            currentStatus += "Multiplying Augment: Active\n";
        }
        if (isHollowingActive)
        {
            currentStatus += "Hollowing Augment: Active\n";
        }
        if (string.IsNullOrEmpty(currentStatus))
        {
            currentStatus = "Augmentless";
        }

        foreach (var augStatusText in augStatusTexts)
        {
            if (augStatusText != null)
            {
                augStatusText.text = currentStatus.TrimEnd('\n');
            }
        }
        Debug.Log("Text updated to: " + currentStatus);
    }

    public void ActivateAugment(string augmentName)
    {
        isAugmentless = false;

        switch (augmentName)
        {
            case "Insurance Augment":
                isInsuranceActive = true;
                isMultiplyingBought = false;
                isHollowingBought = false;
                isMultiplyingActive = false;
                isHollowingActive = false;
                break;

            case "Multiplying Augment":
                isMultiplyingActive = true;
                isInsuranceBought = false;
                isHollowingBought = false;
                isInsuranceActive = false;
                isHollowingActive = false;
                break;

            case "Hollowing Augment":
                isHollowingActive = true;
                isInsuranceBought = false;
                isMultiplyingBought = false;
                isInsuranceActive = false;
                isMultiplyingActive = false;
                break;
        }

        if (!isInsuranceActive && !isMultiplyingActive && !isHollowingActive)
        {
            isAugmentless = true;
        }

        SaveAugments();
    }

    public void ResetAugments()
    {
        PlayerPrefs.DeleteKey("InsuranceBought");
        PlayerPrefs.DeleteKey("InsuranceActive");
        PlayerPrefs.DeleteKey("InsuranceOnEffect");

        PlayerPrefs.DeleteKey("MultiplyingBought");
        PlayerPrefs.DeleteKey("MultiplyingActive");
        PlayerPrefs.DeleteKey("MultiplyingOnEffect");

        PlayerPrefs.DeleteKey("HollowingBought");
        PlayerPrefs.DeleteKey("HollowingActive");
        PlayerPrefs.DeleteKey("HollowingOnEffect");

        PlayerPrefs.DeleteKey("Augmentless");

        isInsuranceBought = false;
        isInsuranceActive = false;
        isInsuranceOnEffect = false;

        isMultiplyingBought = false;
        isMultiplyingActive = false;
        isMultiplyingOnEffect = false;

        isHollowingBought = false;
        isHollowingActive = false;
        isHollowingOnEffect = false;

        isAugmentless = true;
        DisplayCurrentAugments();
        SaveAugments();
    }

    public void SaveAugments()
    {
        PlayerPrefs.SetInt("InsuranceBought", isInsuranceBought ? 1 : 0);
        PlayerPrefs.SetInt("InsuranceActive", isInsuranceActive ? 1 : 0);
        PlayerPrefs.SetInt("InsuranceOnEffect", isInsuranceOnEffect ? 1 : 0);

        PlayerPrefs.SetInt("MultiplyingBought", isMultiplyingBought ? 1 : 0);
        PlayerPrefs.SetInt("MultiplyingActive", isMultiplyingActive ? 1 : 0);
        PlayerPrefs.SetInt("MultiplyingOnEffect", isMultiplyingOnEffect ? 1 : 0);

        PlayerPrefs.SetInt("HollowingBought", isHollowingBought ? 1 : 0);
        PlayerPrefs.SetInt("HollowingActive", isHollowingActive ? 1 : 0);
        PlayerPrefs.SetInt("HollowingOnEffect", isHollowingOnEffect ? 1 : 0);

        PlayerPrefs.SetInt("Augmentless", isAugmentless ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadAugments()
    {
        isInsuranceBought = PlayerPrefs.GetInt("InsuranceBought", 0) == 1;
        isInsuranceActive = PlayerPrefs.GetInt("InsuranceActive", 0) == 1;
        isInsuranceOnEffect = PlayerPrefs.GetInt("InsuranceOnEffect", 0) == 1;

        isMultiplyingBought = PlayerPrefs.GetInt("MultiplyingBought", 0) == 1;
        isMultiplyingActive = PlayerPrefs.GetInt("MultiplyingActive", 0) == 1;
        isMultiplyingOnEffect = PlayerPrefs.GetInt("MultiplyingOnEffect", 0) == 1;

        isHollowingBought = PlayerPrefs.GetInt("HollowingBought", 0) == 1;
        isHollowingActive = PlayerPrefs.GetInt("HollowingActive", 0) == 1;
        isHollowingOnEffect = PlayerPrefs.GetInt("HollowingOnEffect", 0) == 1;

        isAugmentless = PlayerPrefs.GetInt("Augmentless", 0) == 1;
        DisplayCurrentAugments();
        SaveAugments();
    }
}