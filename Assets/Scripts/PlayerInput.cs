using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState
{
    Play,
    Stop
}

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string horizontalKey = "Horizontal";
    [SerializeField] private string verticalKey = "Vertical";
    [SerializeField] private KeyCode rushKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode bubbleKey = KeyCode.Space;
    [SerializeField] private KeyCode interactKey = KeyCode.Space;
    [SerializeField] private KeyCode fishBookKey = KeyCode.Escape;
    [SerializeField] private KeyCode skipKey = KeyCode.Escape;

    public static float Horizontal = 0;
    public static float Vertical = 0;
    public static bool RushDown = false;
    public static bool RushUp = false;
    public static bool BubbleDown = false;
    public static bool Interact = false;
    public static bool FishBook = false;
    public static bool Skip = false;

    public static InputState inputState = InputState.Play;

    private void Update()
    {
        if (inputState == InputState.Play)
        {
            Horizontal = Input.GetAxis(horizontalKey);
            Vertical = Input.GetAxis(verticalKey);
            RushDown = Input.GetKeyDown(rushKey);
            RushUp = Input.GetKeyUp(rushKey);
            //BubbleDown = Input.GetKeyDown(bubbleKey);
            Interact = Input.GetKeyDown(interactKey);
            FishBook = Input.GetKeyDown(fishBookKey);
            Skip = false;
        }
        else if (inputState == InputState.Stop)
        {
            Horizontal = 0;
            Vertical = 0;
            RushDown = false;
            RushUp = true;
            //BubbleDown = false;
            Interact = false;
            FishBook = false;
            Skip = Input.GetKeyDown(skipKey);
        }    
    }
}
