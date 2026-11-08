using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BodyChanger : MonoBehaviour
{
    public BodyManager bodyManager;
    public float[] scales = { 1f, 1.5f };
    public AudioSource sizeSound;

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
        index = (index + 1) % scales.Length;
        bodyManager.SetBody(scales[index]);

        if (sizeSound != null)
        {
            sizeSound.pitch = 1f / scales[index];
            sizeSound.Play();
        }
    }
}