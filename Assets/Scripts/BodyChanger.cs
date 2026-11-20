// VR button that changes the size of the avatar when pressed (the mirror)
// cycles through the list of sizes to grow or shrink the avatar
// tells BodyManager script to scale the player
// body changer sound gets deeper for bigger sizes
// sizes, sounds and hint set up in inspector

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BodyChanger : MonoBehaviour
{
    public BodyManager bodyManager;
    public float[] scales = { 1f, 1.5f };
    public AudioSource sizeSound;
    public GameObject hint;

    private int index = 0;
    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelected);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
    }

    void OnSelected(SelectEnterEventArgs args)
    {
        if (hint != null) hint.SetActive(false);

        // move to next size and cycle back to first size after list ends
        index = (index + 1) % scales.Length;
        bodyManager.SetBody(scales[index]);

        if (sizeSound != null)
        {
            // bigger body gives deeper sound
            // smaller body gives higher sound
            sizeSound.pitch = 1f / scales[index];
            sizeSound.Play();
        }
    }
}