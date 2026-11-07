using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetIfFallen : MonoBehaviour
{
    public float fallLimit = -2f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody body;
    private XRGrabInteractable grab;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        body = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            ResetToStart();
        }
    }

    public void ResetToStart()
    {
        // let go of it first, otherwise the controller or socket keeps holding it
        if (grab != null && grab.interactionManager != null)
        {
            grab.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grab);
        }

        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}