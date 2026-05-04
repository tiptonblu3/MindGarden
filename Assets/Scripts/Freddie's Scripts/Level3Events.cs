using System;
using System.Collections;
using UnityEngine;

public class Level3Events : MonoBehaviour
{
    public GlidePickUp playerGlide;
    public CheckPoints checkPoints;
    public EyeWaypoints eyeWaypoints;
    public BirdWaypoints birdWaypoints;
    public EyeChase eyeChase;
    public GameObject Sun;

    public GameObject CheckPoint3;
    public GameObject Wings;

    public ParticleSystem groundFog;
    public ParticleSystem skyFog;
    public Material Skybox_Alt;

    public GameObject EndLevelTrigger;

    [Header("DeathBox Settings")]
    public Transform deathBox;
    private float targetYScale;
    private float growDuration = 2;

    [Header("Fog Settings")]
    public ParticleSystem fog;
    private float targetYPosition;
    private float moveDuration = 2;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
        eyeWaypoints = GameObject.FindGameObjectWithTag("Eye").GetComponent<EyeWaypoints>();
        birdWaypoints = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdWaypoints>();
        eyeChase = GameObject.FindGameObjectWithTag("Eye").GetComponent<EyeChase>();
    }

    // Update is called once per frame
    void Update()
    {
        EventSetter();
    }

    public void EventSetter()
    {
        
        if (checkPoints.CurrentCheckPointIndex == 0)
        {
            birdWaypoints.currentWaypointIndex = 0; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
        }

        else if (checkPoints.CurrentCheckPointIndex == 1)
        {
            playerGlide.IsGlideUnlocked = true;
            birdWaypoints.currentWaypointIndex = 1; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            Wings.SetActive(false);

            targetYScale = 145;
            StartCoroutine(GrowDeathBox());
        }

        #region Checkpoint 2 Events
        else if (checkPoints.CurrentCheckPointIndex == 2)
        {
            playerGlide.IsGlideUnlocked = true;
            birdWaypoints.currentWaypointIndex = 2; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            Point2();
            Wings.SetActive(false);

            targetYScale = 260;
            StartCoroutine(GrowDeathBox());
        }
        #endregion

        #region Checkpoint 3 Events
        else if (checkPoints.CurrentCheckPointIndex == 3)
        {
            Point2();
            playerGlide.IsGlideUnlocked = true;
            birdWaypoints.currentWaypointIndex = 3; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            eyeWaypoints.currentWaypointIndex = 0; // Set the eye's target waypoint to the first one
            eyeWaypoints.isMoving = true;
            CheckPoint3.SetActive(false);
            Wings.SetActive(false);

            targetYScale = 465;
            targetYPosition = 165;
            StartCoroutine(GrowDeathBox());
            StartCoroutine(MoveFogY());
        }
        #endregion
        
        #region Checkpoint 4 Events
        else if (checkPoints.CurrentCheckPointIndex == 4)
        {
            Point2();
            playerGlide.IsGlideUnlocked = true;
            birdWaypoints.currentWaypointIndex = 4; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            Invoke("StartEyeChase", 2f); // Invoke the StartEyeChase method after a delay of 2 seconds
            EndLevelTrigger.SetActive(true);
            //Invoke("FogOff", 5f);
            CheckPoint3.SetActive(false);
            Wings.SetActive(false);
            eyeWaypoints.finalCheckpointReached = true;

            targetYScale = 20;
            targetYPosition = -50;
            StartCoroutine(GrowDeathBox());
            StartCoroutine(MoveFogY());
        }
        #endregion
    }

    // GrowDeathBox
    #region

    // Makes the Death Fog grow over time
    IEnumerator GrowDeathBox()
    {
        Vector3 startScale = deathBox.localScale;
        Vector3 targetScale = new Vector3(startScale.x, targetYScale, startScale.z);

        float elapsed = 0f;

        while (elapsed < growDuration)
        {
            float t = elapsed / growDuration;
            deathBox.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        deathBox.localScale = targetScale;
    }

    #endregion

    // MoveFogY
    #region

    // Moves the Fog up and down
    IEnumerator MoveFogY()
    {
        Vector3 startPos = fog.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, targetYPosition, startPos.z);

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            fog.transform.position = Vector3.Lerp(startPos, targetPos, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        fog.transform.position = targetPos;
    }

    #endregion

    public void StartEyeChase()
    {
        eyeChase.Chase = true;
    }

    public void FogOff()
    {
        skyFog.gameObject.SetActive(false);
        groundFog.gameObject.SetActive(false);
    }

    public void Point2()
    {
        // Disable the Sun and change the skybox enable the vfx
        Sun.SetActive(false);
        RenderSettings.fog = true;

        if (Skybox_Alt != null)
        {
        Debug.Log("Skybox switch triggered");
        RenderSettings.skybox = Skybox_Alt;
        DynamicGI.UpdateEnvironment();
        }

        if (groundFog != null)
        {
            groundFog.gameObject.SetActive(true);
            groundFog.Play();
        }

        if (skyFog != null)
        {
            skyFog.gameObject.SetActive(true);
            skyFog.Play();
        }
    }
}
