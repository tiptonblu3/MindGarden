using UnityEngine;

public class Level3Events : MonoBehaviour
{
    public CheckPoints checkPoints;
    public EyeWaypoints eyeWaypoints;
    public BirdWaypoints birdWaypoints;
    public EyeChase eyeChase;
    public GameObject Sun;

    public ParticleSystem groundFog;
    public ParticleSystem skyFog;
    public Material Skybox_Alt;

    public GameObject EndLevelTrigger;



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
            birdWaypoints.currentWaypointIndex = 1; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
        }

        #region Checkpoint 2 Events
        else if (checkPoints.CurrentCheckPointIndex == 2)
        {
            birdWaypoints.currentWaypointIndex = 2; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            Point2();
        }
        #endregion

        #region Checkpoint 3 Events
        else if (checkPoints.CurrentCheckPointIndex == 3)
        {
            birdWaypoints.currentWaypointIndex = 3; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            eyeWaypoints.currentWaypointIndex = 0; // Set the eye's target waypoint to the first one
            eyeWaypoints.isMoving = true;
        }
        #endregion
        
        #region Checkpoint 4 Events
        else if (checkPoints.CurrentCheckPointIndex == 4)
        {
            birdWaypoints.currentWaypointIndex = 4; // Set the bird's target waypoint to the first one
            birdWaypoints.isMoving = true;
            Invoke("StartEyeChase", 2f); // Invoke the StartEyeChase method after a delay of 2 seconds
            EndLevelTrigger.SetActive(true);
            Invoke("FogOff", 5f);
        }
        #endregion
    }

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
