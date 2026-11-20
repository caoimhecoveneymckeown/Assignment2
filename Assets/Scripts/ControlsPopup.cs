// shows a control panel on game start that disappears after a few seconds
// pressing 'M' toggles panel on and off

using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsPopup : MonoBehaviour
{
    public GameObject panel;        
    public float showSeconds = 10f;   

    void Start()
    {
        panel.SetActive(true);
        Invoke(nameof(Hide), showSeconds);
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.mKey.wasPressedThisFrame)
        {
            CancelInvoke();                       
            panel.SetActive(!panel.activeSelf);   
        }
    }

    void Hide()
    {
        panel.SetActive(false);
    }
}