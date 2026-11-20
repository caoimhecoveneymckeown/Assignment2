// plays a sound when the player grabs the key
// only player grab sound not for key in socket sound

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeySounds : MonoBehaviour
{
    public AudioSource grabSound;
    public float grabVolume = 1.5f;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrabbed);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRSocketInteractor) return;
        if (grabSound != null) grabSound.PlayOneShot(grabSound.clip, grabVolume);
    }
}