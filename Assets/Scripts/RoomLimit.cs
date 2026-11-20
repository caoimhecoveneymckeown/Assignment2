// stops player from leaving the room through a wall
// uses a box collider as the room boundary
// if playe head leaves boundary they are pushed sideways until back in the boundaries

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

        // how far head is outside boundary 
        Vector3 push = new Vector3(inside.x - head.x, 0f, inside.z - head.z);

        if (push != Vector3.zero)
        {
            xrOrigin.position += push;
        }
    }
}