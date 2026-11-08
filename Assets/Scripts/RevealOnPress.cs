using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RevealOnPress : MonoBehaviour
{
    public GameObject objectToReveal;

    [Header("Painting swing")]
    public Transform paintingHinge;
    public float openAngle = -110f;
    public float openTime = 1f;
    public float swingDelay = 3f;
    public AudioSource paintingSound;

    private XRSimpleInteractable interactable;
    private bool hasRevealed = false;
    public AudioSource pressSound;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelected);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
    }

    void OnSelected(SelectEnterEventArgs args)
    {
        if (hasRevealed) return;
        hasRevealed = true;

        if (pressSound != null) pressSound.Play();

        objectToReveal.SetActive(true);

        if (paintingHinge != null)
            StartCoroutine(SwingPainting());
    }

    IEnumerator SwingPainting()
    {
        yield return new WaitForSeconds(swingDelay);
        if (paintingSound != null) paintingSound.Play();
        Quaternion start = paintingHinge.localRotation;
        Quaternion end = start * Quaternion.Euler(0f, openAngle, 0f);
        float timer = 0f;

        while (timer < openTime)
        {
            timer += Time.deltaTime;
            paintingHinge.localRotation = Quaternion.Slerp(start, end, timer / openTime);
            yield return null;
        }

        paintingHinge.localRotation = end;
    }
}
