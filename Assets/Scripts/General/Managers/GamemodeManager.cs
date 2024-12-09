using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance;

    private const string FILELESS_MALWARE_DONE_KEY = "FilelessMalwareDone";
    private const string ADWARE_DONE_KEY = "AdwareDone";
    private const string VIRUS_DONE_KEY = "VirusDone";
    private const string ROOTKIT_DONE_KEY = "RootkitDone";
    private const string BOTS_DONE_KEY = "BotsDone";
    private const string WORM_DONE_KEY = "WormDone";

    [Header("Status")]
    public bool filelessMalwareDone;
    public bool adwareDone;
    public bool virusDone;
    public bool rootkitDone;
    public bool botsDone;
    public bool wormDone;

    [Header("Buttons")]
    public GameObject filelessButton;
    public GameObject adwareButton;
    public GameObject virusButton;
    public GameObject rootkitButton;
    public GameObject botsButton;
    public GameObject wormButton;

    [Header("Icons")]
    public GameObject malwareFL;
    public GameObject malwareADWARE;
    public GameObject malwareVIRUS;
    public GameObject malwareROOTKIT;
    public GameObject malwareBOTS;
    public GameObject malwareWORM;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
        //progress restarter
        if (SceneManager.GetActiveScene().name == "VisNov_Prologue" || SceneManager.GetActiveScene().name == "MainMenu")
        {
            ResetGMProgress();
            Debug.Log("GM:scene name is prologue");
        }
        else
        {
            LoadGMProgress();
            Debug.Log("GM:scene name is else");
        }

        if (filelessButton != null)
        {
            InvokeRepeating("UpdateFilelessButton", 1f, 1f);
            Debug.Log("GM:filessbtn not null");
        }
        else
        {
            filelessMalwareDone = false;
            Debug.LogWarning("GM:filessbtn is null");
        }

        if (adwareButton != null)
        {
            InvokeRepeating("UpdateAdwareButton", 1f, 1f);
            Debug.Log("GM:adwarebtn not null");
        }
        else
        {
            adwareDone = false;
            Debug.LogWarning("GM:adwarebtn is null");
        }

        if (virusButton != null)
        {
            InvokeRepeating("UpdateVirusButton", 1f, 1f);
            Debug.Log("GM:virusbtn not null");
        }
        else
        {
            virusDone = false;
            Debug.LogWarning("GM:virusbtn is null");
        }

 /*       if (rootkitButton != null)
        {
            InvokeRepeating("UpdateRookitButton", 1f, 1f);
            Debug.Log("GM:rootkitbtn not null");
        }
        else
        {
            rootkitDone = false;
            Debug.LogWarning("GM:rootkitbtn is null");
        }

        if (botsButton != null)
        {
            InvokeRepeating("UpdateBotsButton", 1f, 1f);
            Debug.Log("GM:botsbtn not null");
        }
        else
        {
            botsDone = false;
            Debug.LogWarning("GM:bots is null");
        }
 */

        if (wormButton != null)
        {
            InvokeRepeating("UpdateWormButton", 1f, 1f);
            Debug.Log("GM:wormbtn not null");
        }
        else
        {
            wormDone = false;
            Debug.LogWarning("GM:wormbtn is null");
        }

        //icon check

        if (malwareFL != null)
        {
            if (filelessMalwareDone)
            {
                malwareFL.SetActive(true);
            }
        }

        if (malwareADWARE != null)
        {
            if (adwareDone)
            {
                malwareADWARE.SetActive(true);
            }
        }

        if (malwareVIRUS != null)
        {
            if (virusDone)
            {
                malwareVIRUS.SetActive(true);
            }
        }

        /*if (malwareROOTKIT != null)
        {
            if (rootkitDone)
            {
                malwareROOTKIT.SetActive(true);
            }
        }

        if (malwareBOTS != null)
        {
            if (botsDone)
            {
                malwareBOTS.SetActive(true);
            }
        }*/

        if (malwareWORM != null)
        {
            if (wormDone)
            {
                malwareWORM.SetActive(true);
            }
        }

        UpdateAllBtns();
    }

    public void adwareGM_Done()
    {
        adwareDone = true;
        SaveGMProgress();
    }

    public void filelessGM_Done()
    {
        filelessMalwareDone = true;
        SaveGMProgress();
    }

    public void virusGM_Done()
    {
        virusDone = true;
        SaveGMProgress();
    }

    /*public void rootkitGM_Done()
    {
        rootkitDone = true;
        SaveGMProgress();
    }

    public void botsGM_Done()
    {
        botsDone = true;
        SaveGMProgress();
    }*/

    public void wormGM_Done()
    {
        wormDone = true;
        SaveGMProgress();
    }

    public void LoadGMProgress()
    {
        filelessMalwareDone = PlayerPrefs.GetInt(FILELESS_MALWARE_DONE_KEY, 0) == 1;
        adwareDone = PlayerPrefs.GetInt(ADWARE_DONE_KEY, 0) == 1;
        virusDone = PlayerPrefs.GetInt(VIRUS_DONE_KEY, 0) == 1;
/*        rootkitDone = PlayerPrefs.GetInt(ROOTKIT_DONE_KEY, 0) == 1;
        botsDone = PlayerPrefs.GetInt(BOTS_DONE_KEY, 0) == 1;*/
        wormDone = PlayerPrefs.GetInt(WORM_DONE_KEY, 0) == 1;

        SaveGMProgress();
    }

    public void SaveGMProgress()
    {
        PlayerPrefs.SetInt(FILELESS_MALWARE_DONE_KEY, filelessMalwareDone ? 1 : 0);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, adwareDone ? 1 : 0);
        PlayerPrefs.SetInt(VIRUS_DONE_KEY, virusDone ? 1 : 0);
/*        PlayerPrefs.SetInt(ROOTKIT_DONE_KEY, rootkitDone ? 1 : 0);
        PlayerPrefs.SetInt(BOTS_DONE_KEY, botsDone ? 1 : 0);*/
        PlayerPrefs.SetInt(WORM_DONE_KEY, wormDone ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void ResetGMProgress()
    {
        filelessMalwareDone = false;
        adwareDone = false;
        virusDone = false;
/*        rootkitDone = false;
        botsDone = false;*/
        wormDone = false;
        
        PlayerPrefs.SetInt(FILELESS_MALWARE_DONE_KEY, 0);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, 0);
        PlayerPrefs.SetInt(VIRUS_DONE_KEY, 0);
/*        PlayerPrefs.SetInt(ROOTKIT_DONE_KEY, 0);
        PlayerPrefs.SetInt(BOTS_DONE_KEY, 0);*/
        PlayerPrefs.SetInt(WORM_DONE_KEY, 0);

        PlayerPrefs.Save();
        Debug.Log("GM:gamemodes progress reset");
    }

    public void UpdateButton(GameObject buttonGameObject, bool booleanValue)
    {
        Button buttonComponent = buttonGameObject.GetComponent<Button>();

        buttonComponent.interactable = !booleanValue;
        SaveGMProgress();
        LoadGMProgress();
    }

    public void UpdateAllBtns()
    {
        UpdateAdwareButton();
        UpdateFilelessButton();
        UpdateVirusButton();
/*        UpdateRootkitButton();
        UpdateBotsButton();*/
        UpdateWormButton();
    }

    public void UpdateFilelessButton()
    {
        UpdateButton(filelessButton, filelessMalwareDone);
    }

    public void UpdateAdwareButton()
    {
        UpdateButton(adwareButton, adwareDone);
    }

    public void UpdateVirusButton()
    {
        UpdateButton(virusButton, virusDone);
    }

/*    public void UpdateRootkitButton()
    {
        UpdateButton(rootkitButton, rootkitDone);
    }

    public void UpdateBotsButton()
    {
        UpdateButton(botsButton, botsDone);
    }*/

    public void UpdateWormButton()
    {
        UpdateButton(wormButton, wormDone);
    }
}