using UnityEngine;
using TMPro;

public class discoDialogue : MonoBehaviour, IInteractable
{
    
    public int dialogueIndex = 0;
    public int subIndex = 0;
    public NighmarManager NM; //Nightmare manager script that changes the scene to be nightmare mode
    

    public TextMeshPro Dialogue;
    public GameObject DialogBackUI;
    public PlatformManager platmanscript;
     public AudioSource musicSource;

#region things to dissappear
    public GameObject NPCDissappear;
    public GameObject DilogueDissappear;
    

#endregion



#region Dialogue Arrays
    public string[] firstConvo = { 
        "",
        "Hey kid, ready to tango?", 
        "The goal is to jump on the right platforms to get to the end",
        "of the dance floor! Whenever your ready talk to me again", 
        "and we will get this party started!",  
    };
    public string[] secondConvo = { 
        "Lets do this!",   
    };
    public string[] thirdConvo = { 
        "",
        "Nice Dancing! What were you looking for again?", 
        "The Disc Pieces?.. I think I have some in my studio but...", 
        "Unfortunately a vengeful fan screwed up my setup.",  
        "If you can fix it, you can have my disc pieces!",  
    };
    public string[] fourthConvo = { 
        "What are you waiting for?",   
    };
    public string[] fifthConvo = { 
        "",

        
        "G̶̨̧̛̠͓̘͓̺͉̤̮̠̿͌ͧ͂̂ͯͦ̃̅ͮ́͂̿̇̐̆ͯ͘͘ę̨̭̼̎̏̅͆͆͋t̵̴̸̫͕̘͔̘͍̃͌͛̊ͧ́̓ͨ̋̉̂̕͟͡ͅ r̶͍̘̞̰̳ͥ̎ͮͨ̀̕͞_̜͈̱́͐_̟̉ȩ͖͉̌͗͌̾a̧̗͌d̷̸̵̖̹̘̝̜͚͓̙͎̜͗̋͒̓ͨ̂ͥ͆̋͆̐̕͡y̧͎̦̲̙͉̝̗̲̆͑ͥͨ̆̄̒͗̔̂̈́͢ ṫ̶̴̡̢̛͕̝̘̭̣̱̘̠͇ͩ̑ͣ́͑ͫ̈̒̏ͦ͑̀ͬ͐̀̐̎́̿̊͒́͢ȯ̷̷̖͈̲͍͍͈̮͙̗̻̼̏ͧ̓͊̽̓ͮ̔͘ g͓̘̮̼̹̯ͯ̌͐͂́͒̌͢͟ę̡̪̯̼̖̞̘̞͇̟̭̜̬͗̾̂̽ͯ̔͊̂͗̆͑͗ͩͨͩ͋̉̇ͨ͋̚͜ţ̵̰͔̙͙̗͎͉̱̟̮͂͂̈ͤͦͥ͂ͪ͊̑͌͟_ͣ̓ͬ C̟̪̻͓͈̖͛͂ͦ̍͋͋ͬ͂ͫ͌ͬͣͯ̕͡R̵̛̯̲͖̺̼̯̖͉̯͚̩̞͎̲̬̗͇͑͐́͋̌͆̽̌ͯ̿ͣͤ̃͌͐̈́̈ͥ͋ͪ́͢͠Ù͖͈̒̕S̫̺̻̗̆ͣḨ̧̗̻͙̪̣͍͉̞͎̫̠͚̦͕̭͙͈̞̣̤̰̎ͪͧͮ̇͗̎̃ͧ̾̅̑ͥ̅͛̓̐͠͡E̛̹̻ͧͥ̚͞_̶͓̼̀ͤD̶̢̛̛͈̤̗͙̞͇̽̆͑̀̂͌̇ͮ̏̕", 
    
    
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
            subIndex++;
            DialogueStart();
        }
        
    }

    public void DialogueStart()
    {
        switch (dialogueIndex)
        {
            case 0:
                if (HandleConversation(firstConvo)) dialogueIndex++;
                break;
            case 1:
                if (ConvoDiscoStart(secondConvo)) dialogueIndex++;
                break;
            case 2:
                if (HandleConversation(thirdConvo)) dialogueIndex++;
                break;
            case 3:
                // Special condition for Nightmare Manager
                bool finished = HandleConversation(fourthConvo);
                if (finished && NM.isNighmarActive) dialogueIndex++;
                break;
            case 4:
                if (ConvoDiscoStart(fifthConvo)) dialogueIndex++;
                break;
            default:
                Dialogue.text = "This is awkward, I lost my dialogue text";
                break;
        }

            
        
    }

    bool HandleConversation(string[] currentArray)
    {
        
        if (subIndex < currentArray.Length)
        {
            musicSource.Play();
            string textToDisplay = currentArray[subIndex];
            Dialogue.text = textToDisplay;
            return false; // Not finished yet
        }
        else
        {
            EndDialogue();
            return true; // Finished this array
        }
    }

    bool ConvoDiscoStart(string[] currentArray)
    {
        
        if (subIndex < currentArray.Length)
        {
            string textToDisplay = currentArray[subIndex];
            Dialogue.text = textToDisplay;
            return false; // Not finished yet
        }
        else
        {
            EndDialogue();
            platmanscript.StartDiscoSequence();
            DilogueDissappear.SetActive(false);
            NPCDissappear.SetActive(false);
            if (dialogueIndex == 3) NM.hasInitializedNightmare = true; // Start nightmare mode if it's the right dialogue
            return true; // Finished this array
        }
    }

    void EndDialogue()
    {
        subIndex = 0;
        DialogBackUI.SetActive(false);
        Dialogue.enabled = false;
    }




}
