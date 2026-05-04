using UnityEngine;
using TMPro;


public class BunniDialogue : MonoBehaviour, IInteractable
{
        public int BunDialogueIndex = 0;
    public int BunSubIndex = 0;
    

    public TextMeshPro Dialogue;
    public GameObject DialogBackUI;
    public AudioSource musicSource;





#region Dialogue Arrays
    public string[] BunfirstConvo = { 
        "",
        "Woah, that was close", 
        "If you had gotten to the end of the dance floor..",
        "you would have been trapped in this dream!", 
        "Lets go back to the hub where its safe",  
    };
#endregion
    
    

    public void Interact()
    {
        if (!DialogBackUI.activeSelf)
        {
            DialogBackUI.SetActive(true);
            Dialogue.enabled = true;
            DialogueStart();
        }
        else
        {
            // If it's already on, move to the next line
            BunSubIndex++;
            DialogueStart();
        }
        
    }

    public void DialogueStart()
    {
        switch (BunDialogueIndex)
        {
            case 0:
                if (HandleConversation(BunfirstConvo)) BunDialogueIndex++;
                break;
            default:
                Dialogue.text = "";
                break;
        }
        
    }

    bool HandleConversation(string[] currentArray)
    {
        
        if (BunSubIndex < currentArray.Length)
        {
            musicSource.Play();
            string textToDisplay = currentArray[BunSubIndex];
            Dialogue.text = textToDisplay;
            return false; // Not finished yet
        }
        else
        {
            EndDialogue();
            return true; // Finished this array
        }
    }

    

    void EndDialogue()
    {
        BunSubIndex = 0;
        DialogBackUI.SetActive(false);
        Dialogue.enabled = false;
    }

}
