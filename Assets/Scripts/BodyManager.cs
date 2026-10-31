using UnityEngine;
using UnityEngine.InputSystem;

public class BodyManager : MonoBehaviour
{
    [Header("What to resize")]
    public Transform xrOrigin;

    [Header("Body sizes")]
    public float tallScale = 1.5f;
    public float normalScale = 1f;
    public float smallScale = 0.5f;
    public float biggestScale = 1.85f;

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.f1Key.wasPressedThisFrame) SetBody(tallScale);
        if (keyboard.f2Key.wasPressedThisFrame) SetBody(normalScale);
        if (keyboard.f3Key.wasPressedThisFrame) SetBody(smallScale);
        if (keyboard.f4Key.wasPressedThisFrame) SetBody(biggestScale);

    }

    void SetBody(float scale)
    {
        if (xrOrigin == null)
        {
            Debug.LogWarning("Warning - no XR Origin assigned.");
            return;
        }

        xrOrigin.localScale = Vector3.one * scale;
        Debug.Log("Body size set to " + scale);
    }
}