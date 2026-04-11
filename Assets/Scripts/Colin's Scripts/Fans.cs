using UnityEngine;

public class Fans : MonoBehaviour
{
    // Variables
    #region

    // For the force that pushes the player
    public float pushForce = 15f;
    public float maxPushSpeed = 10f;

    // Boolean that determines if the fan is facing upwards
    public bool isFacingUpwards = true;

    // For controlling the color of the trigger.
    public Color triggerColorSolid = Color.green;
    public Color triggerColorOutline = new Color(0, 1, 0, 0.25f);

    #endregion

    // OnTriggerStay
    #region

    // Sees if the player has entered the trigger, then pushes them in the direction of the Y axis.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Collider col = GetComponent<Collider>();
                Vector3 velocity = rb.linearVelocity;

                float topY = col.bounds.max.y;
                float playerY = other.transform.position.y;

                // Stop upward motion at top of fan
                if (playerY >= topY && isFacingUpwards)
                {
                    velocity.y = 0f;
                    rb.linearVelocity = velocity;
                    return;
                }

                // Cancel downward velocity
                if (velocity.y < 0 && isFacingUpwards)
                {
                    velocity.y = 0f;
                    rb.linearVelocity = velocity;
                }

                // Apply upward force
                if (velocity.y < maxPushSpeed)
                {
                    rb.AddForce(transform.up * pushForce, ForceMode.Acceleration);
                }
                else
                {
                    velocity.y = maxPushSpeed;
                    rb.linearVelocity = velocity;
                }
            }
        }
    }

    #endregion

    // OnDrawGizmos
    #region

    // Purely for visual debugging of the fan's trigger area. Colors it green.
    void OnDrawGizmos()
    {
        Gizmos.color = triggerColorSolid;
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
        }

        Gizmos.color = triggerColorOutline;
        Gizmos.DrawCube(col.bounds.center, col.bounds.size);
    }

    #endregion
}
