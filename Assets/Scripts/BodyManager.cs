// controls player size by scalig XR Origin
// SetBody glides smoothly to a new size and stops player from drifting when body size changes
// f1/f2/f3 keys assigned as debug keys for testing the different sizes

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BodyManager : MonoBehaviour
{
    [Header("What to resize")]
    public Transform xrOrigin;
    public bool enableDebugKeys = true;

    [Header("Body sizes")]
    public float tallScale = 1.5f;
    public float normalScale = 1f;
    public float smallScale = 0.5f;

    [Header("Size change")]
    public float changeTime = 1.5f;

    private Coroutine resizing;

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;
        if (!enableDebugKeys) return;

        if (keyboard.f1Key.wasPressedThisFrame) SetBody(tallScale);
        if (keyboard.f2Key.wasPressedThisFrame) SetBody(normalScale);
        if (keyboard.f3Key.wasPressedThisFrame) SetBody(smallScale);
    }

    // called by mirror to start changing size
    public void SetBody(float scale)
    {
        if (xrOrigin == null)
        {
            // warning message if XR Origin slot is not assigned
            Debug.LogWarning("Warning - no XR Origin assigned.");
            return;
        }

        if (resizing != null) StopCoroutine(resizing);
        resizing = StartCoroutine(GlideTo(scale));
        Debug.Log("Body size set to " + scale);
    }
    
    // blends from current size to target size 
    IEnumerator GlideTo(float target)
    {
        Transform head = Camera.main.transform;
        float start = xrOrigin.localScale.y;
        float timer = 0f;

        while (timer < changeTime)
        {
            timer += Time.deltaTime;
            // smooth transition so not a sharp jump between avatar height change
            float t = Mathf.SmoothStep(0f, 1f, timer / changeTime);
            ApplyScale(Mathf.Lerp(start, target, t), head);
            yield return null;
        }

        ApplyScale(target, head);
    }

    // Scale the origin then shift so head stays at the same spot
    void ApplyScale(float s, Transform head)
    {
        Vector3 before = head.position;
        xrOrigin.localScale = Vector3.one * s;
        Vector3 shift = before - head.position;
        shift.y = 0f;               // only correct sideways drift not height
        xrOrigin.position += shift;
    }
}