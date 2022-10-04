using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour {

    [SerializeField]
    private string conversationStartNode;
    public Vector3 dialogueOffset;

    private DialogueRunner dialogueRunner;
    private DialogueTransform dialogueTransform;

    [SerializeField]
    private bool interactable = true;
    private bool isCurrentConversation = false;
    private bool isRunning = false;


    public void Start()
    {
        dialogueTransform = FindObjectOfType<DialogueTransform>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    public void Interact()
    {
        if (interactable && !dialogueRunner.IsDialogueRunning)
            StartConversation();
    }

    public void StartConversation(float time)
    {
        if (isCurrentConversation || dialogueRunner.IsDialogueRunning)
            return;

        isCurrentConversation = true;
        Invoke(nameof(StartConversation), time);
    }

    private void StartConversation()
    {
        Debug.Log($"Started conversation with {name}.");

        dialogueTransform.SetPosition(transform.position + dialogueOffset);
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            Debug.Log($"Started conversation with {name}.");
        }
    }

    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + dialogueOffset, 0.1f);
    }
}