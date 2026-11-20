// closes virtual hand into a fist when player presses the grab button
// hand is opened when button is released

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrabPose : MonoBehaviour
{
    public XRBaseController controller;
    public Vector3 curlAxis = new Vector3(0f, 0f, 1f);
    public float fingerAngle = 70f;
    public float thumbAngle = 35f;
    public float speed = 12f;

    private List<Transform> fingers = new List<Transform>();
    private List<Transform> thumbs = new List<Transform>();
    private Dictionary<Transform, Quaternion> startRot = new Dictionary<Transform, Quaternion>();
    private float amount;

    void Awake()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            string n = t.name.ToLower();
            if (n.Contains("tip") || n.Contains("metacarpal")) continue;

            bool bendJoint = n.Contains("proximal") || n.Contains("intermediate") || n.Contains("distal");
            if (!bendJoint) continue;

            if (n.Contains("thumb")) thumbs.Add(t);
            else fingers.Add(t);
            startRot[t] = t.localRotation;
        }
        Debug.Log("HandGrabPose: " + fingers.Count + " finger joints, " + thumbs.Count + " thumb joints");
    }

    void LateUpdate()
    {
        float target = controller != null ? controller.selectInteractionState.value : 0f;
        amount = Mathf.Lerp(amount, target, speed * Time.deltaTime);

        foreach (Transform t in fingers)
            t.localRotation = startRot[t] * Quaternion.Euler(curlAxis * fingerAngle * amount);
        foreach (Transform t in thumbs)
            t.localRotation = startRot[t] * Quaternion.Euler(curlAxis * thumbAngle * amount);
    }
}