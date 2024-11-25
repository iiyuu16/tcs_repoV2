using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    private bool isTriggerEnabled = true;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void StartDialogue()
    {
        if (!isTriggerEnabled)
        {
            Debug.Log("Dialogue trigger is disabled.");
            return;
        }

        Button dialogueButton = GetComponentInChildren<Button>();
        if (dialogueButton != null)
        {
            dialogueButton.interactable = false;
            gameObject.SetActive(false);
        }

        dialogueManager.OpenDialogue(messages, actors);
    }

    public bool HasCompletedDialogue()
    {
        return dialogueManager != null && dialogueManager.DialogueCompleted;
    }

    public void SetTriggerEnabled(bool isEnabled)
    {
        isTriggerEnabled = isEnabled;
    }
}