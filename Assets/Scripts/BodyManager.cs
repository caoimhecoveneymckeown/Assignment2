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

    // Glides to the new size
    public void SetBody(float scale)
    {
        if (xrOrigin == null)
        {
            Debug.LogWarning("Warning - no XR Origin assigned.");
            return;
        }

        if (resizing != null) StopCoroutine(resizing);
        resizing = StartCoroutine(GlideTo(scale));
        Debug.Log("Body size set to " + scale);
    }

    // Jumps straight to the new size (used by the door teleport)
    public void SetBodyInstant(float scale)
    {
        if (xrOrigin == null) return;

        if (resizing != null) StopCoroutine(resizing);
        xrOrigin.localScale = Vector3.one * scale;
    }

    IEnumerator GlideTo(float target)
    {
        float start = xrOrigin.localScale.y;
        float timer = 0f;

        while (timer < changeTime)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, timer / changeTime);
            xrOrigin.localScale = Vector3.one * Mathf.Lerp(start, target, t);
            yield return null;
        }

        xrOrigin.localScale = Vector3.one * target;
    }
}