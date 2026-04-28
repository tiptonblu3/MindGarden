using System.Collections;
using System.Numerics;
using UnityEngine;

public class NighmarManager : MonoBehaviour
{
    public bool isNighmarActive = false;
    public bool hasInitializedNightmare = true;
    public PlatformManager platmanscript;

    public Collider endCollider; //for start area to block you from going back up there after the nightmare starts

    public GameObject oldTrigger; //so it allows you to continue
    public float minCooldown = 0.2f; // Minimum cooldown time
    public Animator animator;
    public discoDialogue discoScript;
    


    public GameObject[] Arrows; //to fix arrows from being infront of platform

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNighmarActive)
        {
            animator.SetBool("NIghtmareSeq", isNighmarActive);
            endCollider.isTrigger = false;
            oldTrigger.SetActive(false);
            FixArrows();
            // You can add any additional logic here that should happen when the nightmare is active
            platmanscript.spawnDirection = new UnityEngine.Vector3(1, 0, 0);
            UnityEngine.Vector3 newStart = new UnityEngine.Vector3(-55f, 3f, -1.894971f);
            platmanscript.stopDistance = -5f;
            platmanscript.StartPosition = newStart;
            discoScript.dialogueIndex = 4; //may not be needed though

            //platmanscript.cooldownTime = 2.5f;
            if (platmanscript.StartPlatform != null)
            {
                // Apply the same vertical drop logic your manager uses
                float verticalDrop = 1.5f; 
                platmanscript.StartPlatform.transform.position = new UnityEngine.Vector3(newStart.x, newStart.y - verticalDrop, newStart.z);
            }


            if (!hasInitializedNightmare)
            {
                hasInitializedNightmare = true;
                StartCoroutine(SpeedUpNightmare());
            }
        }
    }

    IEnumerator SpeedUpNightmare()
    {
        while (isNighmarActive)
        {
            yield return new WaitForSeconds(2f);

            if (platmanscript.cooldownTime > 0.3f)
            {
                platmanscript.cooldownTime -= 0.1f;
            }
        }
    }

    public void FixArrows()
{
    foreach (GameObject obj in Arrows)
    {
        if (obj != null)
        {
            // Move 3 meters based on the object's own forward direction
            UnityEngine.Vector3 currentPos = obj.transform.localPosition;
            if (obj.name == "BlueArrowModel" || obj.name == "PurpleArrowModel") 
            {
                // Snap X to -0.005, leave Y and Z alone
                obj.transform.localPosition = new UnityEngine.Vector3(-0.005f, currentPos.y, currentPos.z);
            }
            else 
            {
                // Snap Z to -0.005, leave X and Y alone
                obj.transform.localPosition = new UnityEngine.Vector3(currentPos.x, currentPos.y, -0.005f);
            }

            // OR: Move 3 meters in World Space Z-axis
            // obj.transform.position += Vector3.forward * 3f;
        }
    }
}

}

