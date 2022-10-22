using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class SceneDirector : MonoBehaviour
{
    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void Update()
    {
        if (PlayerInput.Skip && director.state == PlayState.Playing)
            director.time = director.duration;
    }

    public void StopInput()
    {
        PlayerInput.inputState = InputState.Stop;
    }

    public void PlayInput()
    {
        PlayerInput.inputState = InputState.Play;
    }
}
