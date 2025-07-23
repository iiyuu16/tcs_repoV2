using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance;
    public GameObject finalGameMSG;
    public GameObject evaluationUIPrefab;

    private const string ROOTKIT_DONE_KEY = "RootkitDone";
    private const string FILELESS_MALWARE_DONE_KEY = "FilelessMalwareDone";
    private const string VIRUS_DONE_KEY = "VirusDone";
    private const string BOTS_DONE_KEY = "BotsDone";
    private const string ADWARE_DONE_KEY = "AdwareDone";
    private const string WORM_DONE_KEY = "WormDone";

    [Header("Status")]
    public int rootkitDoneCount = 0;
    public int filelessMalwareDoneCount = 0;
    public int virusDoneCount = 0;
    public int botsDoneCount = 0;
    public int adwareDoneCount = 0;
    public int wormDoneCount = 0;

    [Header("Buttons")]
    public GameObject rootkitButton;
    public GameObject filelessButton;
    public GameObject virusButton;
    public GameObject botsButton;
    public GameObject adwareButton;
    public GameObject wormButton;

    [Header("Icons")]
    public GameObject malwareROOTKIT;
    public GameObject malwareFL;
    public GameObject malwareVIRUS;
    public GameObject malwareBOTS;
    public GameObject malwareADWARE;
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

        //check and update buttons
        CheckAndUpdateButtons();

        //icon check
        UpdateMalwareIcons();

        //msg check
        ShowFinalMSG();
    }
    public void adwareGM_Done()
    {
        adwareDoneCount++;
        SaveGMProgress();
        CheckAndUpdateButtons();
    }

    public void filelessGM_Done()
    {
        filelessMalwareDoneCount++;
        SaveGMProgress();
        CheckAndUpdateButtons();
    }

    public void virusGM_Done()
    {
        virusDoneCount++;
        SaveGMProgress();
        CheckAndUpdateButtons();
    }

    public void rootkitGM_Done()
    {
        rootkitDoneCount++;
        SaveGMProgress();
        CheckAndUpdateButtons();
    }

    public void botsGM_Done()
    {
        botsDoneCount++;
        SaveGMProgress();
        CheckAndUpdateButtons();
    }

    public void wormGM_Done()
    {
        wormDoneCount++;
        SaveGMProgress();
        CheckAndUpdateButtons();
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
        CheckAndUpdateButtons();

        Debug.Log("Data Loaded!");

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
        Debug.Log("Data Saved!");

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

    public void UpdateRootkitButton()
    {
        if (rootkitButton != null)
        {
            Button buttonComponent = rootkitButton.GetComponent<Button>();
            buttonComponent.interactable = rootkitDoneCount <= 0;
        }
    }

    public void UpdateFilelessButton()
    {
        if (filelessButton != null)
        {
            Button buttonComponent = filelessButton.GetComponent<Button>();
            buttonComponent.interactable = (filelessMalwareDoneCount <= 0);
        }
    }

    public void UpdateVirusButton()
    {
        if (virusButton != null)
        {
            Button buttonComponent = virusButton.GetComponent<Button>();
            buttonComponent.interactable = (virusDoneCount <= 0);
        }
    }

    public void UpdateAdwareButton()
    {
        if (adwareButton != null)
        {
            Button buttonComponent = adwareButton.GetComponent<Button>();
            buttonComponent.interactable = (rootkitDoneCount > 0 && filelessMalwareDoneCount > 0 && virusDoneCount > 0 && adwareDoneCount <= 0);
        }
    }

    public void UpdateBotsButton()
    {
        if (botsButton != null)
        {
            Button buttonComponent = botsButton.GetComponent<Button>();
            buttonComponent.interactable = (rootkitDoneCount > 0 && filelessMalwareDoneCount > 0 && virusDoneCount > 0 && botsDoneCount <= 0);
        }
    }

    public void UpdateWormButton()
    {
        if (wormButton != null)
        {
            Button buttonComponent = wormButton.GetComponent<Button>();
            buttonComponent.interactable = (rootkitDoneCount > 0 && filelessMalwareDoneCount > 0 && virusDoneCount > 0 && adwareDoneCount > 0  && botsDoneCount > 0 && wormDoneCount <= 0);
        }
    }

    private void UpdateMalwareIcons()
    {
        malwareROOTKIT.SetActive(rootkitDoneCount > 0);
        malwareFL.SetActive(filelessMalwareDoneCount > 0);
        malwareVIRUS.SetActive(virusDoneCount > 0);
        malwareBOTS.SetActive(botsDoneCount > 0);
        malwareADWARE.SetActive(adwareDoneCount > 0);
        malwareWORM.SetActive(wormDoneCount > 0);
    }

    private void CheckAndUpdateButtons()
    {
        UpdateRootkitButton();
        UpdateFilelessButton();
        UpdateVirusButton();
        UpdateAdwareButton();
        UpdateBotsButton();
        UpdateWormButton();
    }

    private void ShowFinalMSG()
    {
        if (filelessMalwareDoneCount == 1 && adwareDoneCount == 1 && virusDoneCount == 1 && rootkitDoneCount == 1 && botsDoneCount == 1 && wormDoneCount == 1)
        {
            finalGameMSG.SetActive(true);
            Debug.Log("message active");
        }
        else
            Debug.Log("all gms not finished");
    }

    public void EvaluatePlyrProgress(bool force = false)
    {
        int completed = GetCompletedMissionCount();

        if (completed > 0 || force)
        {
            Debug.Log($"[Evaluation] Triggered. Completed: {completed}, Forced: {force}");

            if (evaluationUIPrefab != null)
            {
                evaluationUIPrefab.SetActive(true);

                EvaluationManager evalManager = evaluationUIPrefab.GetComponent<EvaluationManager>();
                if (evalManager != null)
                {
                    evalManager.DisplayRank();
                }
            }
        }
        else
        {
            Debug.Log("[Evaluation] Skipped — No missions completed.");
        }
    }

    public int GetCompletedMissionCount()
    {
        int count = 0;
        if (rootkitDoneCount > 0) count++;
        if (filelessMalwareDoneCount > 0) count++;
        if (virusDoneCount > 0) count++;
        if (botsDoneCount > 0) count++;
        if (adwareDoneCount > 0) count++;
        if (wormDoneCount > 0) count++;
        return count;
    }
}