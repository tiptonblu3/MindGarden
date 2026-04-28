using UnityEngine;

public class FanParticleMatcher : MonoBehaviour
{
    private BoxCollider triggerCollider;
    private ParticleSystem particleSystem;

    void Awake()
    {
        triggerCollider = GetComponent<BoxCollider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void LateUpdate()
    {
        var shape = particleSystem.shape;

        shape.scale = triggerCollider.size;
        particleSystem.transform.localPosition = triggerCollider.center;
    }
}
