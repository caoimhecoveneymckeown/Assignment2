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
    }
}