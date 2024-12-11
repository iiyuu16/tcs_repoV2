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
    public int filelessMalwareDoneCount = 0;
    public int adwareDoneCount = 0;
    public int virusDoneCount = 0;
    public int rootkitDoneCount = 0;
    public int botsDoneCount = 0;
    public int wormDoneCount = 0;

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

        // Check and update buttons
        UpdateFilelessButton();
        UpdateAdwareButton();
        UpdateVirusButton();
        UpdateRootkitButton();
        UpdateBotsButton();
        UpdateWormButton();

        //icon check
        UpdateMalwareIcons();
    }

    public void adwareGM_Done()
    {
        adwareDoneCount++;
        SaveGMProgress();
    }

    public void filelessGM_Done()
    {
        filelessMalwareDoneCount++;
        SaveGMProgress();
    }

    public void virusGM_Done()
    {
        virusDoneCount++;
        SaveGMProgress();
    }

    public void rootkitGM_Done()
    {
        rootkitDoneCount++;
        SaveGMProgress();
    }

    public void botsGM_Done()
    {
        botsDoneCount++;
        SaveGMProgress();
    }

    public void wormGM_Done()
    {
        wormDoneCount++;
        SaveGMProgress();
    }

    public void LoadGMProgress()
    {
        filelessMalwareDoneCount = PlayerPrefs.GetInt(FILELESS_MALWARE_DONE_KEY, 0);
        adwareDoneCount = PlayerPrefs.GetInt(ADWARE_DONE_KEY, 0);
        virusDoneCount = PlayerPrefs.GetInt(VIRUS_DONE_KEY, 0);
        rootkitDoneCount = PlayerPrefs.GetInt(ROOTKIT_DONE_KEY, 0);
        botsDoneCount = PlayerPrefs.GetInt(BOTS_DONE_KEY, 0);
        wormDoneCount = PlayerPrefs.GetInt(WORM_DONE_KEY, 0);

        // Update buttons after loading
        UpdateFilelessButton();
        UpdateAdwareButton();
        UpdateVirusButton();
        UpdateRootkitButton();
        UpdateBotsButton();
        UpdateWormButton();
    }

    public void SaveGMProgress()
    {
        PlayerPrefs.SetInt(FILELESS_MALWARE_DONE_KEY, filelessMalwareDoneCount);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, adwareDoneCount);
        PlayerPrefs.SetInt(VIRUS_DONE_KEY, virusDoneCount);
        PlayerPrefs.SetInt(ROOTKIT_DONE_KEY, rootkitDoneCount);
        PlayerPrefs.SetInt(BOTS_DONE_KEY, botsDoneCount);
        PlayerPrefs.SetInt(WORM_DONE_KEY, wormDoneCount);

        PlayerPrefs.Save();
    }

    public void ResetGMProgress()
    {
        filelessMalwareDoneCount = 0;
        adwareDoneCount = 0;
        virusDoneCount = 0;
        rootkitDoneCount = 0;
        botsDoneCount = 0;
        wormDoneCount = 0;

        PlayerPrefs.SetInt(FILELESS_MALWARE_DONE_KEY, 0);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, 0);
        PlayerPrefs.SetInt(VIRUS_DONE_KEY, 0);
        PlayerPrefs.SetInt(ROOTKIT_DONE_KEY, 0);
        PlayerPrefs.SetInt(BOTS_DONE_KEY, 0);
        PlayerPrefs.SetInt(WORM_DONE_KEY, 0);

        PlayerPrefs.Save();
        Debug.Log("GM:gamemodes progress reset");
    }

    public void UpdateFilelessButton()
    {
        if (filelessButton != null)
        {
            Button buttonComponent = filelessButton.GetComponent<Button>();
            buttonComponent.interactable = filelessMalwareDoneCount <= 0;
        }
    }

    public void UpdateAdwareButton()
    {
        if (adwareButton != null)
        {
            Button buttonComponent = adwareButton.GetComponent<Button>();
            buttonComponent.interactable = adwareDoneCount <= 0;
        }
    }

    public void UpdateVirusButton()
    {
        if (virusButton != null)
        {
            Button buttonComponent = virusButton.GetComponent<Button>();
            buttonComponent.interactable = virusDoneCount <= 0;
        }
    }

    public void UpdateRootkitButton()
    {
        if (rootkitButton != null)
        {
            Button buttonComponent = rootkitButton.GetComponent<Button>();
            buttonComponent.interactable = rootkitDoneCount <= 0;
        }
    }

    public void UpdateBotsButton()
    {
        if (botsButton != null)
        {
            Button buttonComponent = botsButton.GetComponent<Button>();
            buttonComponent.interactable = botsDoneCount <= 0;
        }
    }

    public void UpdateWormButton()
    {
        if (wormButton != null)
        {
            Button buttonComponent = wormButton.GetComponent<Button>();
            buttonComponent.interactable = wormDoneCount <= 0;
        }
    }

    private void UpdateMalwareIcons()
    {
        malwareFL.SetActive(filelessMalwareDoneCount > 0);
        malwareADWARE.SetActive(adwareDoneCount > 0);
        malwareVIRUS.SetActive(virusDoneCount > 0);
        malwareROOTKIT.SetActive(rootkitDoneCount > 0);
        malwareBOTS.SetActive(botsDoneCount > 0);
        malwareWORM.SetActive(wormDoneCount > 0);
    }
}