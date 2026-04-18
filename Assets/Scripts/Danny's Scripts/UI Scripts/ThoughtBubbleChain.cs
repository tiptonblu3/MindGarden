using UnityEngine;

public class ThoughtBubbleChain : MonoBehaviour
{
    public Transform headAnchor;
    public Transform bubbleAnchor; // ThoughBubbles (Parent)
    public Transform[] bubbles; // Index of bubbles, 0 = closest

    public float springStrength = 18f;
    public float damping = 4.5f;
    public float mass = 0.8f;
    public float gravity = -4f;
    public float startSize = .85f;
    public float endSize = 1.5f;

    private Vector3[] _positions;
    private Vector3[] _velocities;

    public float padding = 0.2f; // How close bubbles can get to anchors

    void Awake()
    {
        _positions = new Vector3[bubbles.Length];
        _velocities = new Vector3[bubbles.Length];

        for (int i = 0; i < bubbles.Length; i++)
        {
            float t = (float)i / (bubbles.Length - 1);
            float size = Mathf.Lerp(startSize, endSize, t);
            bubbles[i].localScale = Vector3.one * size;
        }
    }

    public void Activate()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            _positions[i] = headAnchor.position;
            _velocities[i] = Vector3.zero;
            bubbles[i].position = headAnchor.position;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < bubbles.Length; i++)
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

            bubbles[i].position = _positions[i];
        }
    }
}