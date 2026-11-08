using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeySounds : MonoBehaviour
{
    public AudioSource grabSound;

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
        // The door socket also "selects" the key, so skip that case
        if (args.interactorObject is XRSocketInteractor) return;
        if (grabSound != null) grabSound.Play();
    }
}