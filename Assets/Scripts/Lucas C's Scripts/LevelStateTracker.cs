using UnityEngine;

public class LevelStateTracker : MonoBehaviour
{
    //section 1 valves
    public GameObject valve1; //blue valve
    public bool valve1On;
    public GameObject valve2; //red valve
    public bool valve2On;
    public GameObject valve3; //yellow valve
    public bool valve3On;
    //section 2 valves
    public GameObject valve4;
    public bool valve4On;
    public GameObject valve5;
    public bool valve5On;
    public GameObject valve6;
    public bool valve6On;
    //buckets
    public GameObject bucket1;
    public bool bucket1Filled;
    public GameObject bucket2;
    public bool bucket2Filled;
    //nightmare
    public bool nightmareTracker;
    public GameObject gameManager;
    private void Start()
    {
        valve1.GetComponent<ValveBehavior>().valveActive = valve1On;
        valve2.GetComponent<ValveBehavior>().valveActive = valve2On;
        valve3.GetComponent<ValveBehavior>().valveActive = valve3On;
        valve4.GetComponent<ValveBehavior>().valveActive = valve4On;
        valve5.GetComponent<ValveBehavior>().valveActive = valve5On;
        valve6.GetComponent<ValveBehavior>().valveActive = valve6On;
    }
    void Update()
    {
        if(valve1.GetComponent<ValveBehavior>().valveActive == true)
        {
            valve1On = true;
        }
        else
        {
            valve1On = false;
        }
        if (valve2.GetComponent<ValveBehavior>().valveActive == true)
        {
            valve2On = true;
        }
        else
        {
            valve2On = false;
        }
        if (valve3.GetComponent<ValveBehavior>().valveActive == true)
        {
            valve3On = true;
        }
        else
        {
            valve3On = false;
        }
        if (valve4.GetComponent<ValveBehavior>().valveActive == true)
        {
            valve4On = true;
        }
        else
        {
            valve4On = false;
        }
        if (valve5.GetComponent<ValveBehavior>().valveActive == true)
        {
            valve5On = true;
        }
        else
        {
            valve5On = false;
        }
        if (valve6.GetComponent<ValveBehavior>().valveActive == true)
        {
            valve6On = true;
        }
        else
        {
            valve6On = false;
        }
        if(bucket1.GetComponent<BucketBehavior>().blueCheck == true && bucket1.GetComponent<BucketBehavior>().redCheck == true && bucket1.GetComponent<BucketBehavior>().yellowCheck == true)
        {
            bucket1Filled = true;
        }
        else
        {
            bucket1Filled = false;
        }
        if (bucket2.GetComponent<Bucket2Behavior>().blueCheck == true && bucket2.GetComponent<Bucket2Behavior>().redCheck == true && bucket2.GetComponent<Bucket2Behavior>().yellowCheck == true)
        {
            bucket2Filled = true;
        }
        else
        {
            bucket2Filled = false;
        }
        if (gameManager.GetComponent<NightmareChecker>().nightmareActive == true)
        {
            nightmareTracker = true;
        }
        else
        {
            nightmareTracker = false;
        }
    }

}
