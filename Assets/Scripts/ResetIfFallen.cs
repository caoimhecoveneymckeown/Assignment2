// puts an object back where it started if it falls out of the room
// remembers starting position and rotation

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
        // remember starting spot and the components needed
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

    // puts object back to the start
    public void ResetToStart()
    {
        // let go of object first
        if (grab != null && grab.interactionManager != null)
        {
            grab.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grab);
        }

        // stop object from moving or spinning then put back to start position and rotation
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}