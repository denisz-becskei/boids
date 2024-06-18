using UnityEngine;

public class HotkeyController : MonoBehaviour
{
    public static bool IsTrailEnabled = false;
    public static bool IsWhiteMode = false;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            IsTrailEnabled = !IsTrailEnabled;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            IsWhiteMode = !IsWhiteMode;
        }
    }
}
