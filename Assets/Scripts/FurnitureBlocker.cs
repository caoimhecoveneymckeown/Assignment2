using UnityEngine;

public class FurnitureBlocker : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform head;
    public LayerMask obstacleLayers;
    public float radius = 0.25f;
    public float kneeHeight = 0.3f;

    private Collider[] hits = new Collider[16];

    void LateUpdate()
    {
        float scale = xrOrigin.localScale.y;
        float r = radius * scale;

        Vector3 top = head.position;
        Vector3 bottom = new Vector3(top.x, xrOrigin.position.y + kneeHeight * scale, top.z);
        if (top.y <= bottom.y) return;

        int count = Physics.OverlapCapsuleNonAlloc(
            bottom, top, r, hits, obstacleLayers, QueryTriggerInteraction.Collide);

        for (int i = 0; i < count; i++)
        {
            Collider c = hits[i];

            Vector3 p = new Vector3(top.x, Mathf.Clamp(c.bounds.center.y, bottom.y, top.y), top.z);
            Vector3 closest = c.ClosestPoint(p);

            Vector3 away = p - closest;
            away.y = 0f;
            float dist = away.magnitude;

            Vector3 move;
            if (dist > 0.0001f)
            {
                if (dist >= r) continue;
                move = away.normalized * (r - dist);
            }
            else
            {
                Vector3 fromCentre = p - c.bounds.center;
                fromCentre.y = 0f;
                if (fromCentre.sqrMagnitude < 0.0001f) fromCentre = Vector3.right;
                float reach = Mathf.Max(c.bounds.extents.x, c.bounds.extents.z) + r;
                move = fromCentre.normalized * reach;
            }

            xrOrigin.position += move;
            top += move;
            bottom += move;
        }
    }
}