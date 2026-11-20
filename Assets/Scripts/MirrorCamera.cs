// puts a camera on the surface of the mirror looking out into the room
// mirror shows whatever is in front of it like a reflection

using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Transform mirror;

    void LateUpdate()
    {
        Vector3 s = mirror.lossyScale;
        Vector3 normal;
        float thickness;
        if (s.z <= s.x && s.z <= s.y) { normal = mirror.forward; thickness = s.z; }
        else if (s.x <= s.y)          { normal = mirror.right;   thickness = s.x; }
        else                          { normal = mirror.up;      thickness = s.y; }

        // point towards the player
        Vector3 head = Camera.main.transform.position;
        if (Vector3.Dot(normal, head - mirror.position) < 0f) normal = -normal;

        // camera sits on mirror and looks into room
        transform.position = mirror.position + normal * (thickness * 0.5f + 0.02f);
        transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
    }
}