using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VentButton : MonoBehaviour
{
    [Header("Who can press it")]
    public Transform xrOrigin;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;

    [Header("Vent")]
    public Transform ventHinge;
    public Vector3 openAngle = new Vector3(0f, 0f, 90f);
    public float openTime = 1.5f;
    public bool isOpen = false;

    private XRSimpleInteractable interactable;
    
    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnPressed);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnPressed);
    }

    void OnPressed(SelectEnterEventArgs args)
    {
        if (isOpen) return;

        float size = xrOrigin.localScale.y;
        if (size < minScale || size > maxScale)
        {
            Debug.Log("Button can't be pressed at this body size.");
            return;
        }

        StartCoroutine(OpenVent());
    }

    IEnumerator OpenVent()
    {
        isOpen = true;
        Quaternion start = ventHinge.localRotation;
        Quaternion end = start * Quaternion.Euler(openAngle);
        float timer = 0f;

        while (timer < openTime)
        {
            timer += Time.deltaTime;
            ventHinge.localRotation = Quaternion.Slerp(start, end, timer / openTime);
            yield return null;
        }

        ventHinge.localRotation = end;
    }
}