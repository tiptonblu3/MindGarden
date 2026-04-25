using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour, IInteractable
{
    public PlatformManager platmanscript;
    public GameObject PuzzleUI;
    public bool puzzleSolved = false;
    public bool PuzActive = false;
    public bool Submit1 = false;
    public bool Submit2 = false;
    public bool Submit3 = false;
    public bool Submit4 = false;
    public bool Submit5 = false;
    
    public Slider Gain; //Gain Slider
    public Image GainIndicator;

    public Slider Resample; //Resample Slider
    public Image ResampleIndicator;

    public Slider Volume; //Volume Slider
    public Image VolumeIndicator;

    public Slider Pitch; //Pitch Slider
    public Image PitchIndicator;

    public Slider Reverb; //Reverb Slider
    public Image ReverbIndicator;

    void Awake()
    {
        GetComponent<Renderer>().material.color = Color.yellowNice;
    }
    void Update()
    {
        PuzzleCheck();
        if (puzzleSolved == true)
        {
            
            //Door opened
        }
    }
    public void Interact()
    {
         PuzzleUI.SetActive(true);
        Time.timeScale = 0f; // Pause Game
        Cursor.lockState = CursorLockMode.None; // Unlock Cursor
        Cursor.visible = true; // Make Cursor Visible
        PuzActive = true;
        
    }
    public void ExitPuzzle()
    {
        PuzzleUI.SetActive(false);
        Time.timeScale = 1f; // Unpause Game
        Cursor.lockState = CursorLockMode.Locked; // Lock Cursor
        Cursor.visible = false; // Make Cursor Invisible
        PuzActive = false;
    }
    public void PuzzleCheckUpdate()
    {
        float GainVal = Gain.value;
        float ResampleVal = Resample.value;
        float VolumeVal = Volume.value;
        float PitchVal = Pitch.value;
        float ReverbVal = Reverb.value;


        if (GainVal == 2)
            {
            //Deactivate Slider and make button green
            Gain.interactable = false;
            GainIndicator.color = Color.green;
            Submit1 = true;
            }
        if (ResampleVal == 6)
            {
            //Deactivate Slider and make button green
            Resample.interactable = false;
            ResampleIndicator.color = Color.green;
            Submit2 = true;
            }
        if (VolumeVal == 7)
            {
            //Deactivate Slider and make button green
            Volume.interactable = false;
            VolumeIndicator.color = Color.green;
            Submit3 = true;
            }
        if (PitchVal == 9)
            {
                //Deactivate Slider and make button green
                Pitch.interactable = false;
                PitchIndicator.color = Color.green;
                Submit4 = true;
            }
        if (ReverbVal == 3)
            {
                //Deactivate Slider and make button green
                Reverb.interactable = false;
                ReverbIndicator.color = Color.green;
                Submit5 = true;
            }
        

        if(Gain.interactable == false && Reverb.interactable == false && Pitch.interactable == false && Volume.interactable == false && Resample.interactable == false)
                {
                    puzzleSolved = true;
                }



    }
    public void PuzzleComplete()
    {
        Debug.Log("Door is opened!");
      //actually open door  

    }
    public void PuzzleCheck()
    {
        float GainVal = Gain.value;
        float ResampleVal = Resample.value;
        float VolumeVal = Volume.value;
        float PitchVal = Pitch.value;
        float ReverbVal = Reverb.value;
            if (Submit1 == false)
            {
                if (GainVal == 2)
                    {
                        GainIndicator.color = Color.yellow;
                    }
                else GainIndicator.color = new Color32(255, 136, 136, 255);
            }

            if (Submit2 == false)
            {
                if (ResampleVal == 6)
                    {
                        ResampleIndicator.color = Color.yellow;
                    }
                else ResampleIndicator.color = new Color32(255, 136, 136, 255);
            }
            
            if (Submit3 == false)
            {
                if (VolumeVal == 7)
                    {
                        VolumeIndicator.color = Color.yellow;
                    }
                else VolumeIndicator.color = new Color32(255, 136, 136, 255);
            }

            if (Submit4 == false)
            {
                if (PitchVal == 9)
                    {
                        PitchIndicator.color = Color.yellow;
                    }
                else PitchIndicator.color = new Color32(255, 136, 136, 255);
            }
            if (Submit5 == false)
            {
                if (ReverbVal == 3)
                    {
                        ReverbIndicator.color = Color.yellow;
                    }
                else ReverbIndicator.color = new Color32(255, 136, 136, 255);
            }
    }


}
