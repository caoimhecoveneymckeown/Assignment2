using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RevealOnPress : MonoBehaviour
{
    public GameObject objectToReveal;

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
        objectToReveal.SetActive(true);
    }
}
