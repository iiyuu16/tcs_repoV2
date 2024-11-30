using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainSceneController : MonoBehaviour
{
    private ParticleTransition particleTransition;
    public float delayTimeToPlay;
    public float delayTimeToTransition;

    public GameObject objTransition;
    public string targetSceneName;

    private void Awake()
    {
        particleTransition = FindObjectOfType<ParticleTransition>();
        if (particleTransition == null)
        {
            Debug.Log("No ParticleTransition found in the scene.");
        }

        if (SceneManager.GetActiveScene().name == "LoadingScreenToADWARE")
        {
            ADWARE_gamemode();
        }

        if (SceneManager.GetActiveScene().name == "LoadingScreenToFLM")
        {
            FLM_gamemode();
        }

        if (SceneManager.GetActiveScene().name == "LoadingScreenToWORM")
        {
            WORM_gamemode();
        }

        if (SceneManager.GetActiveScene().name == "LoadingScreenToVIRUS")
        {
            VIRUS_gamemode();
        }

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "LoadingScreen")
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                StartCoroutine(DelayedSceneTransition());
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
        StartCoroutine(DelayedSceneTransition());
        StartCoroutine(DelayedObjTransition());
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void toVisNovPrologue()
    {
        SceneManager.LoadScene("VisNov_Prologue");
    }

    public void toVisNovMain()
    {
        StartCoroutine(DelayToVNMain());
        StartCoroutine(DelayedObjTransition());
    }

    public void toLoadingSceneFLM()
    {
        StartCoroutine(DelayToLoadingSceneFLM());
        StartCoroutine(DelayedObjTransition());
    }

    public void toLoadingSceneADWARE()
    {
        StartCoroutine(DelayToLoadingSceneADWARE());
        StartCoroutine(DelayedObjTransition());
    }

    public void toLoadingSceneWORM()
    {
        StartCoroutine(DelayToLoadingSceneWORM());
        StartCoroutine(DelayedObjTransition());
    }
    public void toLoadingSceneVIRUS()
    {
        StartCoroutine(DelayToLoadingSceneVIRUS());
        StartCoroutine(DelayedObjTransition());
    }

    public void toVisNov_FLM()
    {
        StartCoroutine(DelayToFLM());
        StartCoroutine(DelayedObjTransition());
    }

    public void FLM_gamemode()
    {
        StartCoroutine(DelayToFLM_gamemode());
        StartCoroutine(DelayedObjTransition());
    }

    public void toVisNov_ADWARE()
    {
        StartCoroutine(DelayToADWARE());
        StartCoroutine(DelayedObjTransition());
    }

    public void ADWARE_gamemode()
    {
        StartCoroutine(DelayToADWARE_gamemode());
        StartCoroutine(DelayedObjTransition());
    }

    public void toVisNov_WORM()
    {
        StartCoroutine(DelayToWORM());
        StartCoroutine(DelayedObjTransition());
    }

    public void toVisNov_VIRUS()
    {
        StartCoroutine(DelayToVIRUS());
        StartCoroutine(DelayedObjTransition());
    }

    public void VIRUS_gamemode()
    {
        StartCoroutine(DelayToVIRUS_gamemode());
        StartCoroutine(DelayedObjTransition());
    }

    public void WORM_gamemode()
    {
        StartCoroutine(DelayToWORM_gamemode());
        StartCoroutine(DelayedObjTransition());
    }

    IEnumerator DelayToVNMain()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("VisNov_Main");
    }

    IEnumerator DelayToLoadingSceneFLM()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("LoadingScreenToFLM");
    }

    IEnumerator DelayToLoadingSceneADWARE()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("LoadingScreenToADWARE");
    }
    IEnumerator DelayToLoadingSceneWORM()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("LoadingScreenToWORM");
    }

    IEnumerator DelayToLoadingSceneVIRUS()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("LoadingScreenToVIRUS");
    }

    IEnumerator DelayToFLM()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("VisNov_FLM");
    }

    IEnumerator DelayToFLM_gamemode()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("s&dGM");
    }

    IEnumerator DelayToADWARE()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("VisNov_ADWARE");
    }

    IEnumerator DelayToADWARE_gamemode()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("rhythmGM");
    }

    IEnumerator DelayToWORM()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("VisNov_WORM");
    }

    IEnumerator DelayToWORM_gamemode()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("wormGM");
    }

    IEnumerator DelayToVIRUS()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("VisNov_VIRUS");
    }

    IEnumerator DelayToVIRUS_gamemode()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("survivalGM");
    }

    IEnumerator DelayedSceneTransition()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name is empty.");
        }

    }

    IEnumerator DelayedObjTransition()
    {
        yield return new WaitForSeconds(delayTimeToTransition);
        objTransition.SetActive(true);
    }
}
