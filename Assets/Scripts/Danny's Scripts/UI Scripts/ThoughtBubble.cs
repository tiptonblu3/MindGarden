using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    // Bubble animation variables
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.1f;
    public float floatSpeed = 1.5f;
    public float floatAmount = 0.05f;

    private Vector3 startScale;
    private Vector3 startPos;

    void Start()
    {
        startScale = transform.localScale;
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Pulsing scale
        float pulse = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = startScale * pulse;

        // Gentle floating
        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.localPosition = startPos + new Vector3(0, floatOffset, 0);
    }
}