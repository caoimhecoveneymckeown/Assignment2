// plays footstep sounds as the player moves
// based on how far player headset moves
// random clip from a list plays with every player step
// stridel ength and pitch scale with the body size of the player (bigger body takes longer steps)
// ignores big jumps and stops sound when player stands still

using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform head;
    public AudioSource source;
    public AudioClip[] clips;
    public float firstStepFraction = 0.25f;
    public float strideLength = 0.7f;
    public float volume = 0.6f;
    public float maxStepSeconds = 0.5f;

    private Vector3 lastPos;
    private float distance;
    private float idleTime;
    private float playStart;

    Vector3 FlatHead()
    {
        Vector3 p = head.position;
        p.y = 0f;
        return p;
    }

    void Start()
    {
        lastPos = FlatHead();
    }

    void Update()
    {
        Vector3 now = FlatHead();
        float moved = (now - lastPos).magnitude;
        lastPos = now;

        // ignore sudden and large jumps
        if (moved > 1f) return;

        // Stop the sound when standing still
        if (moved < 0.003f)
        {
            idleTime += Time.deltaTime;
            if (idleTime > 0.15f)
            distance = strideLength * xrOrigin.localScale.y * (1f - firstStepFraction);
            }
            
        else idleTime = 0f;

        if (source.isPlaying &&
            (idleTime > 0.15f || Time.time - playStart > maxStepSeconds))
            source.Stop();

        distance += moved;
        float scale = xrOrigin.localScale.y;

        if (distance >= strideLength * scale && clips.Length > 0)
        {
            distance = 0f;
            source.Stop();
            source.clip = clips[Random.Range(0, clips.Length)];
            source.pitch = 1f / scale;
            source.volume = volume;
            source.Play();
            playStart = Time.time;
        }
    }
}
