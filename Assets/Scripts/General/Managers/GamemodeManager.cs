using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance;

    public bool filelessMalwareDone;
    public bool adwareDone;
    public bool wormDone;
    public bool virusDone;

    private const string FILELESS_MALWARE_DONE_KEY = "FilelessMalwareDone";
    private const string ADWARE_DONE_KEY = "AdwareDone";
    private const string WORM_DONE_KEY = "WormDone";
    private const string VIRUS_DONE_KEY = "VirusDone";

    public GameObject filelessButton;
    public GameObject adwareButton;
    public GameObject wormButton;
    public GameObject virusButton;

    public GameObject malwareFL;
    public GameObject malwareADWARE;
    public GameObject malwareWorm; // temp obj
    public GameObject malwareVirus;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //progress restarter
        if (SceneManager.GetActiveScene().name == "VisNov_Prologue")
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

        if (malwareWorm != null)
        {
            if (wormDone)
            {
                malwareWorm.SetActive(true);
            }
        }

        if (malwareVirus != null)
        {
            if (virusDone)
            {
                malwareVirus.SetActive(true);
            }
        }

        UpdateAllBtns();
    }

    public void adwareGM_Done()
    {
        adwareDone = true;
        SaveGMProgress();
        LoadGMProgress();
    }

    public void filelessGM_Done()
    {
        filelessMalwareDone = true;
        SaveGMProgress();
        LoadGMProgress();
    }

    public void virusGM_Done()
    {
        virusDone = true;
        SaveGMProgress();
        LoadGMProgress();
    }

    public void worm_Done()
    {
        wormDone = true;
        SaveGMProgress();
        LoadGMProgress();
    }

    public void LoadGMProgress()
    {
        filelessMalwareDone = PlayerPrefs.GetInt(FILELESS_MALWARE_DONE_KEY, 0) == 1;
        adwareDone = PlayerPrefs.GetInt(ADWARE_DONE_KEY, 0) == 1;
        virusDone = PlayerPrefs.GetInt(ADWARE_DONE_KEY, 0) == 1;
        wormDone = PlayerPrefs.GetInt(WORM_DONE_KEY, 0) == 1;

        SaveGMProgress();
    }

    public void SaveGMProgress()
    {
        PlayerPrefs.SetInt(FILELESS_MALWARE_DONE_KEY, filelessMalwareDone ? 1 : 0);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, adwareDone ? 1 : 0);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, virusDone ? 1 : 0);
        PlayerPrefs.SetInt(WORM_DONE_KEY, wormDone ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void ResetGMProgress()
    {
        filelessMalwareDone = false;
        adwareDone = false;
        wormDone = false;
        
        PlayerPrefs.SetInt(FILELESS_MALWARE_DONE_KEY, 0);
        PlayerPrefs.SetInt(ADWARE_DONE_KEY, 0);
        PlayerPrefs.SetInt(WORM_DONE_KEY, 0);
        PlayerPrefs.SetInt(VIRUS_DONE_KEY, 0);

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

    public void UpdateWormButton()
    {
        UpdateButton(wormButton, wormDone);
    }

    public void UpdateVirusButton()
    {
        UpdateButton(virusButton, virusDone);
    }
}