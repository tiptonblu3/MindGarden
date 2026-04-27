using UnityEngine;

public class ThoughtBubbleChain : MonoBehaviour
{
    public Transform headAnchor;
    public Transform bubbleAnchor;  // ThoughtBubbles (Parent)
    public Transform[] bubbles;     // Index of bubbles, 0 = closest

    public float springStrength = 18f;
    public float damping = 4.5f;
    public float mass = 0.8f;
    public float gravity = -4f;
    public float startSize = 0.04f;
    public float endSize = 0.08f;

    private Vector3[] _positions;
    private Vector3[] _velocities;

    public float padding = 0.2f; // How close bubbles can get to anchors

    void Awake()
    {
        _positions = new Vector3[bubbles.Length];
        _velocities = new Vector3[bubbles.Length];

        for (int i = 0; i < bubbles.Length; i++) // Set bubble sizes based on index so they get bigger as they get further from the head
        {
            float t = (float)i / (bubbles.Length - 1);
            float size = Mathf.Lerp(startSize, endSize, t);
            bubbles[i].localScale = Vector3.one * size;
        }
    }

    public void Activate()
    {
        for (int i = 0; i < bubbles.Length; i++) // Start all bubbles at headAnchor with no velocity so they pop out from the head
        {
            _positions[i] = headAnchor.position;
            _velocities[i] = Vector3.zero;
            bubbles[i].position = headAnchor.position;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < bubbles.Length; i++) // For each bubble calculate spring force to target position along line from head to bubble anchor
        {
            float t = (float)i / (bubbles.Length - 1);
            Vector3 target = Vector3.Lerp(headAnchor.position, bubbleAnchor.position, t);

            float droop = Mathf.Sin(t * Mathf.PI) * Mathf.Abs(gravity) * 0.1f;
            target += Vector3.up * droop;

            Vector3 spring = (target - _positions[i]) * springStrength;
            Vector3 dampForce = -_velocities[i] * damping;
            Vector3 accel = (spring + dampForce) / mass;

            _velocities[i] += accel * Time.deltaTime;
            _positions[i] += _velocities[i] * Time.deltaTime;

            // Clamp with padding so bubbles never actually reach the anchors
            Vector3 toAnchor = bubbleAnchor.position - headAnchor.position;
            Vector3 toBubble = _positions[i] - headAnchor.position;
            float projected = Vector3.Dot(toBubble, toAnchor.normalized);
            projected = Mathf.Clamp(projected, padding, toAnchor.magnitude - padding);
            _positions[i] = headAnchor.position + toAnchor.normalized * projected;

        }

        // Give each bubble its own padding so they dont overlap
        for (int i = 1; i < bubbles.Length; i++)
        {
            Vector3 diff = _positions[i] - _positions[i - 1];
            float minDist = (bubbles[i].localScale.x + bubbles[i - 1].localScale.x) * 1.25f;
            if (diff.magnitude < minDist)
            {
                Vector3 correction = diff.normalized * (minDist - diff.magnitude) * 1.25f;
                _positions[i] += correction;
                _positions[i - 1] -= correction;
            }
        }

        // Give corrected position
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].position = _positions[i];
        }
    }
}