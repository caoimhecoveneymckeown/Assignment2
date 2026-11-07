using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Transform mirror;

    void LateUpdate()
    {
        // the mirror's face direction is its thinnest axis
        Vector3 s = mirror.lossyScale;
        Vector3 normal;
        float thickness;
        if (s.z <= s.x && s.z <= s.y) { normal = mirror.forward; thickness = s.z; }
        else if (s.x <= s.y)          { normal = mirror.right;   thickness = s.x; }
        else                          { normal = mirror.up;      thickness = s.y; }

        // make it point out into the room, towards the player
        Vector3 head = Camera.main.transform.position;
        if (Vector3.Dot(normal, head - mirror.position) < 0f) normal = -normal;

        // camera sits on the mirror's face, looking into the room
        transform.position = mirror.position + normal * (thickness * 0.5f + 0.02f);
        transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
    }
}