// deals with the door at the end of the escape room 
// when the correct object (key) is placed in the socket the door swings open on hinge
// when player walks through door, screen fades to black and game finish text is shown
// only the key works and sequence runs only once


using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorUnlock : MonoBehaviour
{
    [Header("Set these in the Inspector")]
    public XRSocketInteractor socket;   
    public Transform requiredKey;       // only the key opens the door

    [Header("Door movement")]
    public Transform hinge;             // door swings around this
    public float openAngle = -90f;
    public float openTime = 1.5f;
    public AudioSource keySound;        
    public float keyTurnDelay = 1f;     // pause before door starts to move
    public AudioSource doorSound;       

    [Header("Ending")]
    public Transform doorway;           // marker just after open door
    public float triggerDistance = 1f;
    public ScreenFader fader;
    public GameObject finishText;

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
        if (isOpen) return;                                           // already open
        if (args.interactableObject.transform != requiredKey) return; // wrong object
        if (keySound != null) keySound.Play();
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;
        yield return new WaitForSeconds(keyTurnDelay);
        if (doorSound != null) doorSound.Play();

        // Swing hinge from start rotation to open angle
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

        // Wait until head is near doorway marker - ignore height
        while (true)
        {
            Vector3 offset = head.position - doorway.position;
            offset.y = 0f;
            if (offset.magnitude < triggerDistance) break;
            yield return null;
        }

        // Hide the hands so they do not show over the black screen
        int handsLayer = LayerMask.NameToLayer("Hands");
        if (handsLayer >= 0) Camera.main.cullingMask &= ~(1 << handsLayer);

        yield return StartCoroutine(fader.Fade(1f));
        if (finishText != null) finishText.SetActive(true);
    }
}