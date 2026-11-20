// fade to black screen for end of game
// value 1 for fully black and value 0 for transparent

using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public float fadeTime = 0.5f;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        SetAlpha(0f);
    }

    public IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = rend.material.color.a;
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, timer / fadeTime));
            yield return null;
        }
        SetAlpha(targetAlpha);
    }

    void SetAlpha(float a)
    {
        rend.material.color = new Color(0f, 0f, 0f, a);
    }
}
