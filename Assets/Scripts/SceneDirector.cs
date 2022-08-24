using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    public void StopInput()
    {
        PlayerInput.inputState = InputState.Stop;
    }

    public void PlayInput()
    {
        PlayerInput.inputState = InputState.Play;
    }
}
