using System.Numerics;
using System.Collections;          // Fixes the IEnumerator error
using UnityEngine;                 // Essential for Unity
using Vector3 = UnityEngine.Vector3;       // Prevents the System.Numerics conflict
using Quaternion = UnityEngine.Quaternion; // Prevents the System.Numerics conflictusing UnityEngine;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour, IInteractable
{
    [Header("General stuff")]
    public PlatformManager platmanscript;
    public GameObject PuzzleUI;
    public bool puzzleSolved = false;
    public bool PuzActive = false;
    private bool DoorClosed = false;
    public GameObject door;
    public CheckPoints CheckPointsScript;
    public AudioSource musicSource;
    public AudioSource DoorSource;

    #region UI Stuff
    [Header("Gain")]
    public Slider Gain; //Gain Slider
    public Image GainIndicator;
    public bool Submit1 = false;

    [Header("Resample")]
    public Slider Resample; //Resample Slider
    public Image ResampleIndicator;
    public bool Submit2 = false;
    
    [Header("Volume")]
    public Slider Volume; //Volume Slider
    public Image VolumeIndicator;
    public bool Submit3 = false;

    [Header("Pitch")]
    public Slider Pitch; //Pitch Slider
    public Image PitchIndicator;
    public bool Submit4 = false;

    [Header("Reverb")]
    public Slider Reverb; //Reverb Slider
    public Image ReverbIndicator;
    public bool Submit5 = false;


    [Header("Solutions")]
    int GainSol;
    int ResampleSol;
    int VolumeSol; 
    int PitchSol; 
    int ReverbSol;

    #endregion


    void Awake()
    {
        RandomizeNums();
    }
    void Update()
    {
        PuzzleCheck();
        if (puzzleSolved && !DoorClosed){
                DoorClosed = true;
                StartCoroutine(PuzzleComplete(UnityEngine.Vector3.forward));
        }
        Checks();
    }
    public void Interact()
    {
        musicSource.Play();
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


        if (GainVal == GainSol)
            {
            //Deactivate Slider and make button green
            Gain.interactable = false;
            GainIndicator.color = Color.green;
            Submit1 = true;
            }
        if (ResampleVal == ResampleSol)
            {
            //Deactivate Slider and make button green
            Resample.interactable = false;
            ResampleIndicator.color = Color.green;
            Submit2 = true;
            }
        if (VolumeVal == VolumeSol)
            {
            //Deactivate Slider and make button green
            Volume.interactable = false;
            VolumeIndicator.color = Color.green;
            Submit3 = true;
            }
        if (PitchVal == PitchSol)
            {
                //Deactivate Slider and make button green
                Pitch.interactable = false;
                PitchIndicator.color = Color.green;
                Submit4 = true;
            }
        if (ReverbVal == ReverbSol)
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
    public IEnumerator PuzzleComplete(UnityEngine.Vector3 axis)
    {
        DoorSource.Play();

        UnityEngine.Quaternion startRotation = door.transform.rotation;
            UnityEngine.Quaternion endRotation = startRotation * Quaternion.Euler(axis * 90);
            float elapsed = 0f;

        while (elapsed < 1.0f)
        {
            door.transform.rotation = UnityEngine.Quaternion.Slerp(startRotation, endRotation, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        door.transform.rotation = endRotation;
        

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
                if (GainVal == GainSol)
                    {
                        GainIndicator.color = Color.yellow;
                    }
                else GainIndicator.color = new Color32(255, 136, 136, 255);
            }

            if (Submit2 == false)
            {
                if (ResampleVal == ResampleSol)
                    {
                        ResampleIndicator.color = Color.yellow;
                    }
                else ResampleIndicator.color = new Color32(255, 136, 136, 255);
            }
            
            if (Submit3 == false)
            {
                if (VolumeVal == VolumeSol)
                    {
                        VolumeIndicator.color = Color.yellow;
                    }
                else VolumeIndicator.color = new Color32(255, 136, 136, 255);
            }

            if (Submit4 == false)
            {
                if (PitchVal == PitchSol)
                    {
                        PitchIndicator.color = Color.yellow;
                    }
                else PitchIndicator.color = new Color32(255, 136, 136, 255);
            }
            if (Submit5 == false)
            {
                if (ReverbVal == ReverbSol)
                    {
                        ReverbIndicator.color = Color.yellow;
                    }
                else ReverbIndicator.color = new Color32(255, 136, 136, 255);
            }
    }
    public void RandomizeNums()
    {
        GainSol = Random.Range(0, 10);
        ResampleSol = Random.Range(0, 10);
        VolumeSol = Random.Range(0, 10);
        PitchSol = Random.Range(0, 10);
        ReverbSol = Random.Range(0, 10);

    }

    public void Checks()
    {
        if (CheckPointsScript.CurrentCheckPointIndex == 3)
        {
            puzzleSolved = true;
        }
    }

}
