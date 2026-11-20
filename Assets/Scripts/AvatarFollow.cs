// makes the avatar follow the player in VR
// avatar stands on the floor under the headset
// turns to where the player is facing (left and right motion only)
// scales so player eye height results in matching height avatar

using UnityEngine;

public class AvatarFollow : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform floor;
    public float modelEyeHeight = 1.6f;
    public float feetOffset = 0f;

    void LateUpdate()
    {
        Transform head = Camera.main.transform;

        float floorY = (floor != null ? floor.position.y : xrOrigin.position.y) + feetOffset;
        float height = head.position.y - floorY;

        transform.position = new Vector3(head.position.x, floorY, head.position.z);
        transform.rotation = Quaternion.Euler(0f, head.eulerAngles.y, 0f);
        transform.localScale = Vector3.one * (height / modelEyeHeight);
    }
}