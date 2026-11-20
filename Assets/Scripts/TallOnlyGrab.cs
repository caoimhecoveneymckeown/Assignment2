// object can only be grabbed once playe avatar is tall
// checks height of XR Origin for this

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TallOnlyGrab : MonoBehaviour
{
    public Transform xrOrigin;       
    // how tall the player must be before grabbing the object   
    public float minScaleToGrab = 1.4f; 

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        bool isTall = xrOrigin.localScale.y >= minScaleToGrab;

        // switch grab on or off depending on avatar height
        if (grab.enabled != isTall)
        {
            grab.enabled = isTall;
        }
    }
}