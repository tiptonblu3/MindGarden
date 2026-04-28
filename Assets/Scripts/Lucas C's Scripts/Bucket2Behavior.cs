using System.Collections;
using UnityEngine;

public class Bucket2Behavior : MonoBehaviour
{
    public GameObject pipe1;//blue pipe 1
    public GameObject pipe2;//blue pipe 2
    public GameObject pipe3;//red puzzle
    public GameObject pipe4;//yellow puzzle
    public bool blueCheck;
    public bool redCheck;
    public bool yellowCheck;
    public GameObject blueWater;
    public GameObject redWater;
    public GameObject yellowWater;
    public GameObject barricadeDoor;
    public bool bucketFilled;
    public void Start()
    {
        if (bucketFilled == true)
        {
            ToggleBucket();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (blueCheck == true && redCheck == true && yellowCheck == true)
        {
            ToggleBucket();
            return;
        }
        if (pipe1.GetComponent<WaterPipeBehavior>().correctPosition == 1 && pipe2.GetComponent<WaterPipeBehavior>().correctPosition == 1)
        {
            blueCheck = true;
            blueWater.SetActive(true);
        }
        else
        {
            blueCheck = false;
            blueWater.SetActive(false);
        }
        if (pipe3.GetComponent<WaterPipeBehavior>().correctPosition == 1)
        {
            redCheck = true;
            redWater.SetActive(true);
        }
        else
        {
            redCheck = false;
            redWater.SetActive(false);
        }
        if (pipe4.GetComponent<WaterPipeBehavior>().correctPosition == 1)
        {
            yellowCheck = true;
            yellowWater.SetActive(true);
        }
        else
        {
            yellowCheck = false;
            yellowWater.SetActive(false);
        }
    }

    public void ToggleBucket()
    {
        Debug.Log("Bucket is now toggled");
        //Destroy(this.gameObject);
        StartCoroutine(AnimateDoor());
        //barricadeDoor.transform.position = new Vector3(0, 0, 0);
    }
    private IEnumerator AnimateDoor()
    {
        Vector3 startPosition = barricadeDoor.transform.position;
        Vector3 endPosition = new Vector3(0, -10, 0);
        for (float t = 0; t < 1; t += Time.deltaTime * 1f)
        {
            barricadeDoor.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        barricadeDoor.transform.position = endPosition;
    }

}