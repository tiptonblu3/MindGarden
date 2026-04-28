using UnityEngine;
using TMPro;

public class discoDialogue : MonoBehaviour, IInteractable
{
    
    public int dialogueIndex = 0;
    public int subIndex = 0;
    public NighmarManager NM; //Nightmare manager script that changes the scene to be nightmare mode
    

    public TextMeshProUGUI Dialogue;
    public GameObject DialogBackUI;

#region Dialogue Arrays
    public string[] firstConvo = { 
        "Hey kid, ready to tango?", 
        "The goal is to jump on the right platforms to get to the end of the disco.", 
        "Whenever your ready talk to me again and we will get this party started!",  
    };
    public string[] secondConvo = { 
        "Lets do this!",   
    };
    public string[] thirdConvo = { 
        "Nice Dancing! What were you looking for again?", 
        "The Disc Pieces?.. I think I have some in my studio but...", 
        "Unfortunately a vengeful fan screwed up my setup. If you can fix it, you can have my disc pieces!",  
    };
    public string[] fourthConvo = { 
        "What are you waiting for?",   
    };
    public string[] fifthConvo = { 


        "G̶̨̧̛̠͓̘͓̺͉̤̮̠̿͌ͧ͂̂ͯͦ̃̅ͮ́͂̿̇̐̆ͯ͘͘ę̨̭̼̎̏̅͆͆͋t̵̴̸̫͕̘͔̘͍̃͌͛̊ͧ́̓ͨ̋̉̂̕͟͡ͅ r̶͍̘̞̰̳ͥ̎ͮͨ̀̕͞_̜͈̱́͐_̟̉ȩ͖͉̌͗͌̾a̧̗͌d̷̸̵̖̹̘̝̜͚͓̙͎̜͗̋͒̓ͨ̂ͥ͆̋͆̐̕͡y̧͎̦̲̙͉̝̗̲̆͑ͥͨ̆̄̒͗̔̂̈́͢ ṫ̶̴̡̢̛͕̝̘̭̣̱̘̠͇ͩ̑ͣ́͑ͫ̈̒̏ͦ͑̀ͬ͐̀̐̎́̿̊͒́͢ȯ̷̷̖͈̲͍͍͈̮͙̗̻̼̏ͧ̓͊̽̓ͮ̔͘ g͓̘̮̼̹̯ͯ̌͐͂́͒̌͢͟ę̡̪̯̼̖̞̘̞͇̟̭̜̬͗̾̂̽ͯ̔͊̂͗̆͑͗ͩͨͩ͋̉̇ͨ͋̚͜ţ̵̰͔̙͙̗͎͉̱̟̮͂͂̈ͤͦͥ͂ͪ͊̑͌͟_ͣ̓ͬ C̟̪̻͓͈̖͛͂ͦ̍͋͋ͬ͂ͫ͌ͬͣͯ̕͡R̵̛̯̲͖̺̼̯̖͉̯͚̩̞͎̲̬̗͇͑͐́͋̌͆̽̌ͯ̿ͣͤ̃͌͐̈́̈ͥ͋ͪ́͢͠Ù͖͈̒̕S̫̺̻̗̆ͣḨ̧̗̻͙̪̣͍͉̞͎̫̠͚̦͕̭͙͈̞̣̤̰̎ͪͧͮ̇͗̎̃ͧ̾̅̑ͥ̅͛̓̐͠͡E̛̹̻ͧͥ̚͞_̶͓̼̀ͤD̶̢̛̛͈̤̗͙̞͇̽̆͑̀̂͌̇ͮ̏̕", 
    
    
    };
#endregion
    
    

    public void Interact()
    {
        DialogueStart();
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void DialogueStart()
    {
        switch (dialogueIndex)
        {
            case 0:
                // Iterate through the specific set
                DialogBackUI.SetActive(true);
                Dialogue.enabled = true;
                Dialogue.text = firstConvo[subIndex]; //starting dialogue
                subIndex++;

                // If we've reached the end of the array, move to the next dialogueIndex
                if (subIndex >= firstConvo.Length)
                {
                    dialogueIndex++;
                    subIndex = 0; // Reset for the next set of dialogue
                }
                DialogBackUI.SetActive(false);
                Dialogue.enabled = false;
                break;

            case 1:
                DialogBackUI.SetActive(true);
                Dialogue.enabled = true;

                Dialogue.text = secondConvo[subIndex]; //start dance sequence
                subIndex++;

                // If we've reached the end of the array, move to the next dialogueIndex
                if (subIndex >= secondConvo.Length)
                {
                    dialogueIndex++;
                    subIndex = 0; // Reset for the next set of dialogue
                }
                DialogBackUI.SetActive(false);
                Dialogue.enabled = false;
                break;

            case 2:
                DialogBackUI.SetActive(true);
                Dialogue.enabled = true;

                Dialogue.text = thirdConvo[subIndex]; //tell player to grab disc pieces
                subIndex++;

                // If we've reached the end of the array, move to the next dialogueIndex
                if (subIndex >= thirdConvo.Length)
                {
                    dialogueIndex++;
                    subIndex = 0; // Reset for the next set of dialogue
                }
                DialogBackUI.SetActive(false);
                Dialogue.enabled = false;
                break;

            case 3:
                DialogBackUI.SetActive(true);
                Dialogue.enabled = true;

                Dialogue.text = fourthConvo[subIndex]; //Waiting for player to start sequence
                subIndex++;

                // If we've reached the end of the array, move to the next dialogueIndex
                if (subIndex >= fourthConvo.Length)
                {
                    if(NM.isNighmarActive == true)dialogueIndex++;
                    subIndex = 0; // Reset for the next set of dialogue
                }
                DialogBackUI.SetActive(false);
                Dialogue.enabled = false;
                break;
            
            case 4:
                DialogBackUI.SetActive(true);
                Dialogue.enabled = true;

                Dialogue.text = fifthConvo[subIndex]; //corrupted text?
                subIndex++;

                // If we've reached the end of the array, move to the next dialogueIndex
                if (subIndex >= fifthConvo.Length)
                {
                    dialogueIndex++;
                    subIndex = 0; // Reset for the next set of dialogue
                }
                DialogBackUI.SetActive(false);
                Dialogue.enabled = false;
                break;

            default:
                DialogBackUI.SetActive(true);
                Dialogue.enabled = true;
                
                Dialogue.text = "This is awkward, I lost my dialogue text";
                
                DialogBackUI.SetActive(false);
                Dialogue.enabled = false;
                break;
        }
    }
}
