using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordPlayer : MonoBehaviour
{
    public AudioSource music;
    public Transform platter;
    public float rpm = 33.33f;
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

        void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("RecordPlayer: selected");
        Toggle();
    }

    public void Toggle()
    {
        Debug.Log("RecordPlayer: Toggle called. music = " +
            (music == null ? "NULL" : (music.clip == null ? "NO CLIP" : music.clip.name)));

        isOn = !isOn;
        if (isOn) music.UnPause();
        else      music.Pause();

        if (isOn && !music.isPlaying) music.Play();
    }

    void Update()
    {
        if (isOn && platter != null)
            platter.Rotate(spinAxis, rpm * 6f * Time.deltaTime, Space.Self);
    }
}