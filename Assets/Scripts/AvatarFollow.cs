using UnityEngine;

public class AvatarFollow : MonoBehaviour
{
    public Transform xrOrigin;

    void LateUpdate()
    {
        Transform head = Camera.main.transform;
        float s = xrOrigin.localScale.y;
        float height = head.position.y - xrOrigin.position.y;

        transform.position = new Vector3(head.position.x, xrOrigin.position.y + height / 2f, head.position.z);
        transform.rotation = Quaternion.Euler(0f, head.eulerAngles.y, 0f);
        transform.localScale = new Vector3(0.4f * s, height / 2f, 0.4f * s);
    }
}