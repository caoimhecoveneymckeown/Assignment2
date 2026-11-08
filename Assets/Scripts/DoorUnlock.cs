using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorUnlock : MonoBehaviour
{
    [Header("Set these in the Inspector")]
    public XRSocketInteractor socket;
    public Transform requiredKey;

    [Header("Door movement")]
    public Transform hinge;
    public float openAngle = -90f;
    public float openTime = 1.5f;
    public AudioSource keySound;
    public float keyTurnDelay = 1f;
    public AudioSource doorSound;

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
        if (keySound != null) keySound.Play();
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;
        yield return new WaitForSeconds(keyTurnDelay);
        if (doorSound != null) doorSound.Play();
        Quaternion start = hinge.localRotation;
        Quaternion end = start * Quaternion.Euler(0f, openAngle, 0f);
        float timer = 0f;

        while (timer < openTime)
        {
            timer += Time.deltaTime;
            hinge.localRotation = Quaternion.Slerp(start, end, timer / openTime);
            yield return null;
        }

        hinge.localRotation = end;
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

        yield return StartCoroutine(fader.Fade(1f));
        xrOrigin.SetPositionAndRotation(destination.position, destination.rotation);
        roomLimit.SetArea(chamber2Area);
        bodyManager.SetBody(bodyManager.normalScale);
        yield return StartCoroutine(fader.Fade(0f));
    }
}