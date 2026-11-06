using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorUnlock : MonoBehaviour
{
    [Header("Set these in the Inspector")]
    public XRSocketInteractor socket;
    public Transform requiredKey;

    [Header("Door movement")]
    public Vector3 openOffset = new Vector3(0f, 2.1f, 0f);
    public float openTime = 1.5f;

    [Header("Going through the door")]
    public Transform doorway;
    public Transform xrOrigin;
    public Transform destination;
    public BodyManager bodyManager;
    public ScreenFader fader;
    public float triggerDistance = 1f;
    public RoomLimit roomLimit;
    public BoxCollider chamber2Area;

    bool isOpen = false;

    void OnEnable()
    {
        if (socket != null) socket.selectEntered.AddListener(OnKeyInserted);
    }

    void OnDisable()
    {
        if (socket != null) socket.selectEntered.RemoveListener(OnKeyInserted);
    }

    void OnKeyInserted(SelectEnterEventArgs args)
    {
        if (isOpen) return;
        if (args.interactableObject.transform != requiredKey) return;
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;
        Vector3 start = transform.position;
        Vector3 end = start + openOffset;
        float timer = 0f;

        while (timer < openTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, timer / openTime);
            yield return null;
        }

        transform.position = end;
        yield return StartCoroutine(WaitForPlayer());
    }

    IEnumerator WaitForPlayer()
    {
        Transform head = Camera.main.transform;

        while (true)
        {
            Vector3 offset = head.position - doorway.position;
            offset.y = 0f;
            if (offset.magnitude < triggerDistance) break;
            yield return null;
        }

        // the loop has ended, so these lines run exactly once
        yield return StartCoroutine(fader.Fade(1f));
        xrOrigin.SetPositionAndRotation(destination.position, destination.rotation);
        roomLimit.SetArea(chamber2Area);
        bodyManager.SetBody(bodyManager.normalScale);
        yield return StartCoroutine(fader.Fade(0f));
    }
}