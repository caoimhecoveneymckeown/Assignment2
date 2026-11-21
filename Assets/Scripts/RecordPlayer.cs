// lets the player switch the record player on and off
// the platter spins at a set RPM when record player is on

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordPlayer : MonoBehaviour
{
    public AudioSource music;                 
    public Transform platter;                 // the part that spins
    public float rpm = 33.33f;                // classic record speed
    public Vector3 spinAxis = Vector3.up;

    private bool isOn;
    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
    }

    void OnEnable()
    {
        if (interactable != null) interactable.selectEntered.AddListener(OnSelected);
    }

    void OnDisable()
    {
        if (interactable != null) interactable.selectEntered.RemoveListener(OnSelected);
    }

    // Selecting record player switches it on or off
    void OnSelected(SelectEnterEventArgs args)
    {
        Toggle();
    }

    public void Toggle()
    {
        isOn = !isOn;
        if (isOn) music.UnPause();
        else      music.Pause();


        if (isOn && !music.isPlaying) music.Play();
    }

    void Update()
    {
        // spin platter while record player on
        if (isOn && platter != null)
            platter.Rotate(spinAxis, rpm * 6f * Time.deltaTime, Space.Self);
    }
}