using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    private AugmentManager augmentManager;
    private ShopManager shopManager;

    private PopUpManager popUpManager;

    // debuffs
    public bool shopInflation;
    public bool nonStopPopUp;

    // buffs
    public bool shopDiscount;

    // normal states (static)
    public bool shopNormal;
    public bool noPopups;

    // status icons
    public GameObject discountIcon; //shop buff
    public GameObject inflationIcon; // shop debuff
    public GameObject popUpIcon;
    private void Awake()
    {
        instance = this;

        shopManager = FindObjectOfType<ShopManager>();
        if (shopManager == null)
        {
            Debug.LogWarning("ShopManager instance not found in the scene.");
        }

        popUpManager = FindObjectOfType<PopUpManager>();
        if (popUpManager == null)
        {
            Debug.LogWarning("PopUpManager instance not found in the scene. PopUpDebuffs will not work.");
        }

        augmentManager = FindObjectOfType<AugmentManager>();
        if (augmentManager == null)
        {
            Debug.LogWarning("AugmentManager instance not found in the scene. PopUpDebuffs will not work.");
        }

        //reset statuses
        if (SceneManager.GetActiveScene().name == "VisNov_Prologue")
        {
           setToDefaultStatus();
           SaveStatus();
        }

        //buff icons
        if (discountIcon != null)
        {
            discountIcon.SetActive(false);
        }

        //debuff icons
        if (popUpIcon != null)
        {
            popUpIcon.SetActive(false);
        }

        if (inflationIcon != null)
        {
            inflationIcon.SetActive(false);
        }

        LoadStatus();
    }


    public void SaveStatus()
    {
        //normal states
        PlayerPrefs.SetInt("ShopNormal", shopNormal ? 1 : 0);
        PlayerPrefs.SetInt("NoPopups", noPopups ? 1 : 0);
        //buffs
        PlayerPrefs.SetInt("ShopDiscount", shopDiscount ? 1 : 0);
        //debuffs
        PlayerPrefs.SetInt("ShopInflation", shopInflation ? 1 : 0);
        PlayerPrefs.SetInt("NonStopPopUp", nonStopPopUp ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadStatus()
    {
        //normal states
        shopNormal = PlayerPrefs.GetInt("ShopNormal", 0) == 1;
        noPopups = PlayerPrefs.GetInt("NoPopups", 0) == 1;
        //buffs
        shopDiscount = PlayerPrefs.GetInt("ShopDiscount", 0) == 1;
        //debuffs
        shopInflation = PlayerPrefs.GetInt("ShopInflation", 0) == 1;
        nonStopPopUp = PlayerPrefs.GetInt("NonStopPopUp", 0) == 1;
    }

    public void setToDefaultStatus()
    {
        shopNormalStatus();
        popupDebuffOff();
        Debug.Log("all statuses are resetted");
    }

    public void Update()
    {
        if (shopInflation)
        {
            shopDebuffOn();
        }

        if (shopDiscount)
        {
            shopBuffOn();
        }

        if (shopNormal)
        {
            shopNormalStatus();
        }

        if (nonStopPopUp)
        {
            popupDebuffOn();
        }

        if (noPopups)
        {
            popupDebuffOff();
        }
    }

    // functs are called by rewardManagers from different gamemodes
    public void shopNormalStatus()
    {
        shopInflation = false;
        shopNormal = true;
        shopDiscount = false;

        PlayerPrefs.SetInt("ShopDiscount", 0);
        PlayerPrefs.SetInt("ShopInflation", 0);
        PlayerPrefs.SetInt("ShopNormal", 1);

        if (inflationIcon != null)
        {
            inflationIcon.SetActive(false);
        }

        if (discountIcon != null)
        {
            discountIcon.SetActive(false);
        }


        SaveStatus();
        LoadStatus();
    }

    public void shopDebuffOn()
    {
        shopInflation = true;
        shopNormal = false;
        shopDiscount = false;

        if (inflationIcon != null)
        {
            inflationIcon.SetActive(true);
        }

        if (discountIcon != null)
        {
            discountIcon.SetActive(false);
        }

        PlayerPrefs.SetInt("ShopDiscount", 0);
        PlayerPrefs.SetInt("ShopInflation", 1);
        PlayerPrefs.SetInt("ShopNormal", 0);


        SaveStatus();
        LoadStatus();
    }

    public void shopBuffOn()
    {
        shopInflation = false;
        shopNormal = false;
        shopDiscount = true;

        if (inflationIcon != null)
        {
            inflationIcon.SetActive(false);
        }

        if (discountIcon != null)
        {
            discountIcon.SetActive(true);
        }

        PlayerPrefs.SetInt("ShopDiscount", 1);
        PlayerPrefs.SetInt("ShopInflation", 0);
        PlayerPrefs.SetInt("ShopNormal", 0);

        SaveStatus();
        LoadStatus();
    }

    public void popupDebuffOn()
    {
        nonStopPopUp = true;
        noPopups = false;

        if (popUpIcon != null)
        {
            popUpIcon.SetActive(true);
        }

        if (popUpManager != null)
        {
            popUpManager.gameObject.SetActive(true);
            popUpManager.OnThisManager();
        }
        else
        {
            Debug.LogWarning("PopUpManager instance not found. PopUpDebuffs will not work.");
        }

        PlayerPrefs.SetInt("NonStopPopUp", 1);
        PlayerPrefs.SetInt("NoPopups", 0);

        SaveStatus();
        LoadStatus();
    }

    public void popupDebuffOff()
    {
        nonStopPopUp = false;
        noPopups = true;

        if (popUpIcon != null)
        {
            popUpIcon.SetActive(false);
        }

        if (popUpManager != null)
        {
            popUpManager.OffThisManager();
            popUpManager.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("PopUpManager instance not found. PopUpDebuffs will not work.");
        }

        PlayerPrefs.SetInt("NonStopPopUp", 0);
        PlayerPrefs.SetInt("NoPopups", 1);

        SaveStatus();
        LoadStatus();
    }
}