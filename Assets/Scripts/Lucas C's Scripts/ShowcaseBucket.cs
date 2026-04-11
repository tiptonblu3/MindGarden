using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseBucket : MonoBehaviour
{
    public Image targetImage;

    // We change these to 'Object' to see if the Inspector "unlocks"
    public Sprite state1;
    public Sprite state2;
    public Sprite statefull;
    public Sprite baseImage;

    public GameObject waterObject; // used to show it's functionality, made to be removed as a referebce

    public int fillLevel = 0;

    private void Start()
    {
        UpdateVisuals();
    }

    public void OnClick()
    {
        fillLevel += 1;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (fillLevel == 0)
        {
            targetImage.sprite = baseImage;
        }
        else if (fillLevel == 1)
        {
            targetImage.sprite = state1;
        }
        else if (fillLevel == 2)
        {
            targetImage.sprite = state2;
        }
        else if (fillLevel >= 3)
        {
            targetImage.sprite = statefull;
            RemoveBarricade();
        }
    }

    private void RemoveBarricade()
    {
        //used for demonstrating it's on and off states, made to be removed as a reference
        waterObject.SetActive(false);
    }
}
