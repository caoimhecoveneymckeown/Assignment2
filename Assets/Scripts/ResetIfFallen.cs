using UnityEngine;

public class ResetIfFallen : MonoBehaviour
{
    public float fallLimit = -2f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody body;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}