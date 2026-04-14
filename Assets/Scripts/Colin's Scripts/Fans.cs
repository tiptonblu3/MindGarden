using UnityEngine;

public class Fans : MonoBehaviour
{
    // Variables
    #region

    // For the force that pushes the player
    public float pushForce = 15f;
    public float maxPushSpeed = 10f;

    // To fix the diagonal fans
    public bool isDiagonal = false;

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
                Vector3 velocity = rb.linearVelocity;

                // Direction of fan
                Vector3 fanDirection = transform.up.normalized;

                // Turn off gravity
                if (isDiagonal)
                {
                    rb.AddForce(-Physics.gravity, ForceMode.Acceleration);
                }
                else
                {
                    Vector3 gravity = Physics.gravity;
                    float gravityAlongFan = Vector3.Dot(gravity, fanDirection);
                    rb.AddForce(-fanDirection * gravityAlongFan, ForceMode.Acceleration);
                }

                // Gets velocity along the fan direction
                float velocityAlongFan = Vector3.Dot(velocity, fanDirection);

                // Cancels any movement in the opposite direction
                if (velocityAlongFan < 0)
                {
                    velocity -= fanDirection * velocityAlongFan;
                    rb.linearVelocity = velocity;
                    velocityAlongFan = 0;
                }

                // Clamps maximum speed along fan direction
                if (velocityAlongFan < maxPushSpeed)
                {
                    rb.AddForce(fanDirection * pushForce, ForceMode.Acceleration);
                }
                else
                {
                    float excess = velocityAlongFan - maxPushSpeed;
                    velocity -= fanDirection * excess;
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