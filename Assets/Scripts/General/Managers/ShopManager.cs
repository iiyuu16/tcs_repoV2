using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    private MoneyManager moneyManager;
    private AugmentManager augmentManager;
    private StatusManager statusManager;

    public GameObject notEnoughMoneyObject;
    public GameObject transactionDeniedObject;
    public GameObject convoManagerObj;

    [Header("Insurance Augment")]
    public int insuranceStartingPrice = 2000;
    public int insuranceCurrentPrice;
    public TextMeshProUGUI insurancePriceText;

    [Header("Multiplying Augment")]
    public int multiplyingStartingPrice = 1500;
    public int multiplyingCurrentPrice;
    public TextMeshProUGUI multiplyingPriceText;

    [Header("Hollowing Augment")]
    public int hollowingStartingPrice = 1800;
    public int hollowingCurrentPrice;
    public TextMeshProUGUI hollowingPriceText;

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

        moneyManager = FindObjectOfType<MoneyManager>();
        if (moneyManager == null)
        {
        }
        else if (SceneManager.GetActiveScene().name == "VisNov_Prologue")
        {
            moneyManager.ResetMoney();
        }

        augmentManager = FindObjectOfType<AugmentManager>();

        statusManager = FindObjectOfType<StatusManager>();
    }

    public void Update()
    {
        checkPrices();
        showPrices();
    }

    public void checkPrices()
    {
        if (insurancePriceText && multiplyingPriceText && hollowingPriceText)
        {
            if (statusManager.shopInflation)
            {
                insuranceCurrentPrice = insuranceStartingPrice + 1400;
                multiplyingCurrentPrice = multiplyingStartingPrice + 1050;
                hollowingCurrentPrice = hollowingStartingPrice + 1260;
            }
            else if (statusManager.shopDiscount)
            {
                insuranceCurrentPrice = insuranceStartingPrice - 300;
                multiplyingCurrentPrice = multiplyingStartingPrice - 225;
                hollowingCurrentPrice = hollowingStartingPrice - 270;
            }
            else
            {
                insuranceCurrentPrice = insuranceStartingPrice;
                multiplyingCurrentPrice = multiplyingStartingPrice;
                hollowingCurrentPrice = hollowingStartingPrice;
            }
        }
    }

    public void showPrices()
    {
        if (insurancePriceText != null)
        {
            insurancePriceText.text = insuranceCurrentPrice.ToString();
        }

        if (multiplyingPriceText != null)
        {
            multiplyingPriceText.text = multiplyingCurrentPrice.ToString();
        }

        if (hollowingPriceText != null)
        {
            hollowingPriceText.text = hollowingCurrentPrice.ToString();
        }
    }

    public void PurchaseInsurance()
    {
        int price;

        if (statusManager.shopInflation)
        {
            price = insuranceStartingPrice + 1400;
        }
        else if (statusManager.shopDiscount)
        {
            price = insuranceStartingPrice - 300;
        }
        else
        {
            price = insuranceStartingPrice;
        }
        

        if (augmentManager.isInsuranceBought)
        {
            ShowTransactionDenied();
            return;
        }

        if (CanAfford(price))
        {
            moneyManager.SpendMoney(price);
            augmentManager.isInsuranceBought = true;

            augmentManager.isInsuranceActive = true;
            augmentManager.isMultiplyingActive = false;
            augmentManager.isHollowingActive = false;

            augmentManager.isAugmentless = false;
            augmentManager.SaveAugments();

            convoManagerObj.SetActive(true);
            augmentManager.DisplayCurrentAugments();
        }
        else
        {
            ShowNotEnoughMoney();
            convoManagerObj.SetActive(false);
        }
    }

    public void PurchaseMultiplying()
    {
        int price;
        
        if (statusManager.shopInflation)
        {
            price = multiplyingStartingPrice + 1050;
        }
        else if (statusManager.shopDiscount)
        {
            price = multiplyingStartingPrice - 225;
        }
        else
        {
            price = multiplyingStartingPrice;
        }

        if (augmentManager.isMultiplyingBought)
        {
            ShowTransactionDenied();
            return;
        }

        if (CanAfford(price))
        {
            moneyManager.SpendMoney(price);
            augmentManager.isMultiplyingBought = true;

            augmentManager.isMultiplyingActive = true;
            augmentManager.isInsuranceActive = false;
            augmentManager.isHollowingActive = false;

            augmentManager.isAugmentless = false;
            augmentManager.SaveAugments();

            convoManagerObj.SetActive(true);
            augmentManager.DisplayCurrentAugments();
        }
        else
        {
            ShowNotEnoughMoney();
            convoManagerObj.SetActive(false);
        }
    }

    public void PurchaseHollowing()
    {
        int price;

        if (statusManager.shopInflation)
        {
            price = hollowingStartingPrice + 1260;
        }
        else if (statusManager.shopDiscount)
        {
            price = hollowingStartingPrice - 270;
        }
        else
        {
            price = hollowingStartingPrice;
        }

        if (augmentManager.isHollowingBought)
        {
            ShowTransactionDenied();
            return;
        }

        if (CanAfford(price))
        {
            moneyManager.SpendMoney(price);
            augmentManager.isHollowingBought = true;

            augmentManager.isHollowingActive = true;
            augmentManager.isMultiplyingActive = false;
            augmentManager.isInsuranceActive = false;

            augmentManager.isAugmentless = false;
            augmentManager.SaveAugments();

            convoManagerObj.SetActive(true);
            augmentManager.DisplayCurrentAugments();
        }
        else
        {
            ShowNotEnoughMoney();
            convoManagerObj.SetActive(false);
        }
    }

    private bool CanAfford(int price)
    {
        if (moneyManager != null)
        {
            return moneyManager.GetCurrentMoney() >= price;
        }
        else
        {
            return false;
        }
    }

    private void ShowNotEnoughMoney()
    {
        if (notEnoughMoneyObject != null)
        {
            convoManagerObj.SetActive(false);
            notEnoughMoneyObject.SetActive(true);
        }
        if (transactionDeniedObject != null)
        {
            transactionDeniedObject.SetActive(false);
        }
    }

    private void ShowTransactionDenied()
    {
        if (notEnoughMoneyObject != null)
        {
            notEnoughMoneyObject.SetActive(false);
        }
        if (transactionDeniedObject != null)
        {
            convoManagerObj.SetActive(false);
            transactionDeniedObject.SetActive(true);
        }
    }
}