using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TallOnlyGrab : MonoBehaviour
{
    public Transform xrOrigin;          
    public float minScaleToGrab = 1.4f; 

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        bool isTall = xrOrigin.localScale.y >= minScaleToGrab;
        if (grab.enabled != isTall)
        {
            grab.enabled = isTall;
        }
    }
}