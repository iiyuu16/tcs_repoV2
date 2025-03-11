using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wormToRetryScene : MonoBehaviour
{
    public float delayTimeToPlay;
    public float delayTimeToTransition;

    public GameObject objTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player in worm");
            StartCoroutine(DelayToRetryScene());
            StartCoroutine(DelayedObjTransition());
        }
    }

    IEnumerator DelayToRetryScene()
    {
        yield return new WaitForSeconds(delayTimeToPlay);
        SceneManager.LoadScene("VisNov_WORM(RETRY)");
    }
    IEnumerator DelayedObjTransition()
    {
        yield return new WaitForSeconds(delayTimeToTransition);
        objTransition.SetActive(true);
    }

}
