// keyboard shortcuts for testing and recovering 
// R restarts the whole game
// K puts the key back in its start position
// C puts the cube (object concealing the button) back in its start position

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameShortcuts : MonoBehaviour
{
    public ResetIfFallen key;
    public ResetIfFallen cube;

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (keyboard.kKey.wasPressedThisFrame) key.ResetToStart();
        if (keyboard.cKey.wasPressedThisFrame) cube.ResetToStart();
    }
}