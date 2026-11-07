using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ComfortToggle : MonoBehaviour
{
    public TunnelingVignetteController vignette;

    private bool isOn = false;
    private SimpleProvider provider = new SimpleProvider();

    // a minimal stand-in for the locomotion provider the vignette normally waits for
    class SimpleProvider : ITunnelingVignetteProvider
    {
        public VignetteParameters vignetteParameters => null;
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.vKey.wasPressedThisFrame)
        {
            isOn = !isOn;
            if (isOn) vignette.BeginTunnelingVignette(provider);
            else vignette.EndTunnelingVignette(provider);
            Debug.Log("Comfort vignette " + (isOn ? "on" : "off"));
        }
    }
}