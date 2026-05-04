using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NPCTalk : MonoBehaviour, IInteractable
{
    public GameObject dialogueBox;
    public List<GameObject> dialogueLines;
    public GameObject player;
    public Rigidbody playerRb;
    public AudioSource Bunni;
    public AudioClip Squee;

    public void Interact()
    {
        AudioSource.PlayClipAtPoint(Squee, transform.position);

       // if the Dialogue box is not  active, activate it and show the first line of dialogue
        if (!dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(true);
            ShowDialogueLine(0);
            // disable player movement during dialogue
            playerRb.isKinematic = true;
        }
        else
        {
            // if the Dialogue box is active, check which line is currently active and show the next one
            for (int i = 0; i < dialogueLines.Count; i++)
            {
                if (dialogueLines[i].activeInHierarchy)
                {
                    dialogueLines[i].SetActive(false);
                    if (i + 1 < dialogueLines.Count)
                    {
                        ShowDialogueLine(i + 1);
                    }
                    else
                    {
                        dialogueBox.SetActive(false); // hide the dialogue box after the last line
                        playerRb.isKinematic = false; // re-enable player movement after dialogue
                    }
                    break;
                }
            }
        }
    }

    private void ShowDialogueLine(int index)
    {
        if (index >= 0 && index < dialogueLines.Count)
        {
            dialogueLines[index].SetActive(true);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // grab player rigidbody and disable it to prevent movement during dialogue
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
