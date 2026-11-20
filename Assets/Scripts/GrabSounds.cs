// plays sounds when player picks up an object
// sound is 3D so the audio comes from the object itslef



using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabSound : MonoBehaviour
{
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;

    private XRGrabInteractable grab;
    private AudioSource src;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 1f;
    }

    void OnEnable()  { grab.selectEntered.AddListener(OnGrabbed); }
    void OnDisable() { grab.selectEntered.RemoveListener(OnGrabbed); }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRSocketInteractor) return;
        if (clip != null) src.PlayOneShot(clip, volume);
    }
}