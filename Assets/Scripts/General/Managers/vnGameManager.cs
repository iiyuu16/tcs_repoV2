using UnityEngine;

public class vnGameManager : MonoBehaviour
{
    [Space]
    public DialogueTrigger TriggerConvo1;
    public DialogueTrigger TriggerConvo2;
    public DialogueTrigger TriggerConvo3;
    public DialogueTrigger TriggerConvo4;
    public DialogueTrigger TriggerConvo5a1;
    public DialogueTrigger TriggerConvo5a2;
    public DialogueTrigger TriggerConvo5b;
    public DialogueTrigger TriggerConvo6;
    [Space]
    public bool Convo1Done = false;
    public bool Convo2Done = false;
    public bool Convo3Done = false;
    public bool Convo4Done = false;
    public bool Convo5a1Done = false;
    public bool Convo5a2Done = false;
    public bool Convo5bDone = false;
    public bool Convo6Done = false;
    [Space]
    public GameObject[] Convo1ObjectsToEnable;
    public GameObject[] Convo1ObjectsToDisable;
    [Space]
    public GameObject[] Convo2ObjectsToEnable;
    public GameObject[] Convo2ObjectsToDisable;
    [Space]
    public GameObject[] Convo3ObjectsToEnable;
    public GameObject[] Convo3ObjectsToDisable;
    [Space]
    public GameObject[] Convo4ObjectsToEnable;
    public GameObject[] Convo4ObjectsToDisable;
    [Space]
    public GameObject[] Convo5a1ObjectsToEnable;
    public GameObject[] Convo5a1ObjectsToDisable;
    [Space]
    public GameObject[] Convo5a2ObjectsToEnable;
    public GameObject[] Convo5a2ObjectsToDisable;
    [Space]
    public GameObject[] Convo5bObjectsToEnable;
    public GameObject[] Convo5bObjectsToDisable;
    [Space]
    public GameObject[] Convo6ObjectsToEnable;
    public GameObject[] Convo6ObjectsToDisable;
    [Space]
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnDialogueCompleted += OnDialogueCompleted;
    }

    void Update()
    {
        UpdateObjectives();
        OnDialogueCompleted();
    }

    void UpdateObjectives()
    {
        if (Convo1Done)
        {
            EnableDisableObjects(Convo1ObjectsToEnable, Convo1ObjectsToDisable);
        }

        if (Convo2Done)
        {
            EnableDisableObjects(Convo2ObjectsToEnable, Convo2ObjectsToDisable);
        }

        if (Convo3Done)
        {
            EnableDisableObjects(Convo3ObjectsToEnable, Convo3ObjectsToDisable);
        }

        if (Convo4Done)
        {
            EnableDisableObjects(Convo4ObjectsToEnable, Convo4ObjectsToDisable);
        }

        if (Convo5a1Done)
        {
            EnableDisableObjects(Convo5a1ObjectsToEnable, Convo5a1ObjectsToDisable);
        }

        if (Convo5a2Done)
        {
            EnableDisableObjects(Convo5a2ObjectsToEnable, Convo5a2ObjectsToDisable);
        }

        if (Convo5bDone)
        {
            EnableDisableObjects(Convo5bObjectsToEnable, Convo5bObjectsToDisable);
        }

        if (Convo6Done)
        {
            EnableDisableObjects(Convo6ObjectsToEnable, Convo6ObjectsToDisable);
        }
    }

    void EnableDisableObjects(GameObject[] objectsToEnable, GameObject[] objectsToDisable)
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    public void convo1Interaction()
    {
        if (!Convo1Done)
        {
            TriggerConvo1.SetTriggerEnabled(true);
            TriggerConvo1.StartDialogue();
        }
    }

    public void convo2Interaction()
    {
        if (!Convo2Done)
        {
            TriggerConvo2.SetTriggerEnabled(true);
            TriggerConvo2.StartDialogue();
        }
    }

    public void convo3Interaction()
    {
        if (!Convo3Done)
        {
            TriggerConvo3.SetTriggerEnabled(true);
            TriggerConvo3.StartDialogue();
        }
    }

    public void convo4Interaction()
    {
        if (!Convo4Done)
        {
            TriggerConvo4.SetTriggerEnabled(true);
            TriggerConvo4.StartDialogue();
        }
    }

    public void convo5a1Interaction()
    {
        if (!Convo5a1Done)
        {
            TriggerConvo5a1.SetTriggerEnabled(true);
            TriggerConvo5a1.StartDialogue();
        }
    }

    public void convo5a2Interaction()
    {
        if (!Convo5a2Done)
        {
            TriggerConvo5a2.SetTriggerEnabled(true);
            TriggerConvo5a2.StartDialogue();
        }
    }

    public void convo5bInteraction()
    {
        if (!Convo5bDone)
        {
            TriggerConvo5b.SetTriggerEnabled(true);
            TriggerConvo5b.StartDialogue();
        }
    }

    public void convo6Interaction()
    {
        if (!Convo6Done)
        {
            TriggerConvo6.SetTriggerEnabled(true);
            TriggerConvo6.StartDialogue();
        }
    }

    private void OnDialogueCompleted()
    {
        Debug.Log("Dialogue completed event triggered");

        if (!Convo1Done && TriggerConvo1.HasCompletedDialogue())
        {
            Convo1Done = true;
        }


        if (!Convo2Done && TriggerConvo2.HasCompletedDialogue())
        {
            Convo2Done = true;
        }


        if (!Convo3Done && TriggerConvo3.HasCompletedDialogue())
        {
            Convo3Done = true;
        }


        if (!Convo4Done && TriggerConvo4.HasCompletedDialogue())
        {
            Convo4Done = true;
        }

        if (!Convo5a1Done && TriggerConvo5a1.HasCompletedDialogue())
        {
            Convo5a1Done = true;
        }

        if (!Convo5a2Done && TriggerConvo5a2.HasCompletedDialogue())
        {
            Convo5a2Done = true;
        }

        if (!Convo5bDone && TriggerConvo5b.HasCompletedDialogue())
        {
            Convo5bDone = true;
        }


        if (!Convo6Done && TriggerConvo6.HasCompletedDialogue())
        {
            Convo6Done = true;
        }
    }

    void OnDestroy()
    {
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueCompleted -= OnDialogueCompleted;
        }
    }
}
