// plays a 3D impact sound when object dropped
// volume scales with impact speed
// cooldown stops repeating sound too quickly when an object bounces

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropSound : MonoBehaviour
{
    // sound scaling for impact speed
    public AudioClip dropClip;
    public float minImpactSpeed = 0.8f;
    public float maxImpactSpeed = 5f;
    public float cooldown = 0.25f;
    public float masterVolume = 0.35f;

    private AudioSource source;
    private XRGrabInteractable grab;
    private float lastPlayTime = -10f;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
        grab = GetComponent<XRGrabInteractable>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (dropClip == null) return;
        if (grab != null && grab.isSelected) return;
        if (Time.time - lastPlayTime < cooldown) return;

        float speed = collision.relativeVelocity.magnitude;
        if (speed < minImpactSpeed) return;

        // max impact speed quiet (0) to loud (1)
        float volume = Mathf.Clamp(Mathf.InverseLerp(minImpactSpeed, maxImpactSpeed, speed), 0.2f, 1f);
        source.PlayOneShot(dropClip, volume * masterVolume);
        lastPlayTime = Time.time;
    }
}