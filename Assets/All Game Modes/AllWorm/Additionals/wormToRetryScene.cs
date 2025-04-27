using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wormToRetryScene : MonoBehaviour
{
    public float delayTimeToPlay;
    public float delayTimeToTransition;

    private GameObject objTransition;
    private Animator transitionAnimator;

    private void Awake()
    {
        GameObject[] allTransitions = GameObject.FindGameObjectsWithTag("Transition");

        if (allTransitions.Length > 0)
        {
            objTransition = allTransitions[0];
            transitionAnimator = objTransition.GetComponent<Animator>();

            if (transitionAnimator == null)
            {
                Debug.LogError("No Animator found on Transition object!");
            }
            else
            {
                transitionAnimator.enabled = false;
            }
        }
        else
        {
            Debug.LogError("Transition object not found! Make sure it has the 'Transition' tag and is active.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered worm trigger");
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

        if (transitionAnimator != null)
        {
            transitionAnimator.enabled = true;
        }
    }
}
