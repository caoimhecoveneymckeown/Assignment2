// hidden button in the escape room
// when pressed it switches on the mirror (hidden behind a painting)
// after short delay painting swings open on hinge to reveal the mirror
// 'press mirror' hint revealed after painting is fully open


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
    public GameObject hint;

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

    // smoothly swing open painting
    IEnumerator SwingPainting()
    {
        yield return new WaitForSeconds(swingDelay);
        if (paintingSound != null) paintingSound.Play();

        // where swing starts and ends
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

        // if painting is open show the hint
        if (hint != null) hint.SetActive(true);
    }
}
