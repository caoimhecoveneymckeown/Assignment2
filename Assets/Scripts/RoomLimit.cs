using UnityEngine;

public class RoomLimit : MonoBehaviour
{
    public Transform xrOrigin;
    public BoxCollider area;

    public void SetArea(BoxCollider newArea)
    {
        area = newArea;
    }

    void LateUpdate()
    {
        if (area == null) return;

        Vector3 head = Camera.main.transform.position;
        Vector3 inside = area.bounds.ClosestPoint(head);
        Vector3 push = new Vector3(inside.x - head.x, 0f, inside.z - head.z);

        if (push != Vector3.zero)
        {
            xrOrigin.position += push;
        }
    }
}