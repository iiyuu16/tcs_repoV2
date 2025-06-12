using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainSceneController : MonoBehaviour
{
    private ParticleTransition particleTransition;
    private glitchManager _glitchManager;

    public float delayTimeToPlay;
    public float delayTimeToTransition;
    public GameObject objTransition;
    public string targetSceneName;

    private void Awake()
    {
        particleTransition = FindObjectOfType<ParticleTransition>();
        _glitchManager = FindAnyObjectByType<glitchManager>();

        if (particleTransition == null)
            Debug.Log("No ParticleTransition found.");

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu" || sceneName == "VisNov_Prologue")
        {
            _glitchManager?.DisableGlitchEffect();
            Debug.Log("glitchOff");
        }

        if (sceneName == "LoadingScreenToADWARE") ADWARE_gamemode();
        if (sceneName == "LoadingScreenToFLM") FLM_gamemode();
        if (sceneName == "LoadingScreenToVIRUS") VIRUS_gamemode();
        if (sceneName == "LoadingScreenToROOTKIT") ROOTKIT_gamemode();
        if (sceneName == "LoadingScreenToBOTS") BOTS_gamemode();
        if (sceneName == "LoadingScreenToWORM") WORM_gamemode();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "LoadingScreen")
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                StartCoroutine(DelayedSceneTransition(targetSceneName));
                StartCoroutine(DelayedObjTransition());
            }
            else
            {
                Debug.LogError("Target scene name is empty.");
            }
        }
    }

    public void Play()
    {
        Debug.Log("Play");
        if (particleTransition != null)
        {
            particleTransition.TriggerTransition();
        }
        StartCoroutine(DelayedSceneTransition(targetSceneName));
        StartCoroutine(DelayedObjTransition());
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void toMainMenu() => StartCoroutine(DelayedSceneTransition("MainMenu"));
    public void toVisNovPrologue() => StartCoroutine(DelayedSceneTransition("VisNov_Prologue"));
    public void toVisNovMain() => StartCoroutine(DelayedSceneTransition("VisNov_Main"));

    public void toLoadingSceneFLM() => StartCoroutine(DelayedSceneTransition("LoadingScreenToFLM"));
    public void toLoadingSceneADWARE() => StartCoroutine(DelayedSceneTransition("LoadingScreenToADWARE"));
    public void toLoadingSceneWORM() => StartCoroutine(DelayedSceneTransition("LoadingScreenToWORM"));
    public void toLoadingSceneVIRUS() => StartCoroutine(DelayedSceneTransition("LoadingScreenToVIRUS"));
    public void toLoadingSceneROOTKIT() => StartCoroutine(DelayedSceneTransition("LoadingScreenToROOTKIT"));
    public void toLoadingSceneBOTS() => StartCoroutine(DelayedSceneTransition("LoadingScreenToBOTS"));

    public void toVisNov_FLM() => StartCoroutine(DelayedSceneTransition("VisNov_FLM"));
    public void toVisNov_ADWARE() => StartCoroutine(DelayedSceneTransition("VisNov_ADWARE"));
    public void toVisNov_WORM() => StartCoroutine(DelayedSceneTransition("VisNov_WORM"));
    public void toVisNov_VIRUS() => StartCoroutine(DelayedSceneTransition("VisNov_VIRUS"));
    public void toVisNov_ROOTKIT() => StartCoroutine(DelayedSceneTransition("VisNov_ROOTKIT"));
    public void toVisNov_BOTS() => StartCoroutine(DelayedSceneTransition("VisNov_BOTS"));
    public void toVisNov_TrueEnding() => StartCoroutine(DelayedSceneTransition("VisNov_TrueEnding"));
    public void toVisNov_GoodEnding() => StartCoroutine(DelayedSceneTransition("VisNov_GoodEnding"));

    // gamemode scenes
    public void FLM_gamemode() => StartCoroutine(DelayedSceneTransition("s&dGM"));
    public void ADWARE_gamemode() => StartCoroutine(DelayedSceneTransition("rhythmGM"));
    public void VIRUS_gamemode() => StartCoroutine(DelayedSceneTransition("survivalGM"));
    public void ROOTKIT_gamemode() => StartCoroutine(DelayedSceneTransition("mazeGM"));
    public void WORM_gamemode() => StartCoroutine(DelayedSceneTransition("WormGM"));
    public void BOTS_gamemode() => StartCoroutine(DelayedSceneTransition("Bots GM"));

    IEnumerator DelayedSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(delayTimeToPlay);

        if (!string.IsNullOrEmpty(sceneName))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Scene name is empty.");
        }
    }

    IEnumerator DelayedObjTransition()
    {
        yield return new WaitForSeconds(delayTimeToTransition);
        if (objTransition != null)
            objTransition.SetActive(true);
    }
}