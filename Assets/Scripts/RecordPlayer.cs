using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public AudioSource music;
    public Transform platter;          
    public float rpm = 33.33f;    
    public Vector3 spinAxis = Vector3.up;     

    private bool isOn;

    public void Toggle()
    {
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