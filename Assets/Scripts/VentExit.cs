using System.Collections;
using UnityEngine;

public class VentExit : MonoBehaviour
{
    public VentButton vent;
    public Transform xrOrigin;
    public ScreenFader fader;
    public GameObject finishText;
    public float maxScale = 0.6f;
    public float triggerDistance = 0.4f;

    private bool finished = false;

    void Update()
    {
        if (finished) return;
        if (!vent.isOpen) return;
        if (xrOrigin.localScale.y > maxScale) return;

        Vector3 offset = Camera.main.transform.position - transform.position;
        offset.y = 0f;
        if (offset.magnitude < triggerDistance)
        {
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        finished = true;
        yield return StartCoroutine(fader.Fade(1f));
        finishText.SetActive(true);
    }
}
