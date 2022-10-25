using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    [SerializeField] private AudioSource bgmPlayer;
    [SerializeField] private AudioSource sfxPlayerPrefab;
    private List<AudioSource> sfxPlayerList = new List<AudioSource>();

    private void AddSFXPlayer()
    {
        AudioSource audioSource = Instantiate(sfxPlayerPrefab, transform);
        sfxPlayerList.Add(audioSource);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmPlayer.clip = clip;
        bgmPlayer.Play();
        //StopCoroutine(nameof(PlayBGMRoutine));
        //StartCoroutine(nameof(PlayBGMRoutine), clip);
    }

    private IEnumerator PlayBGMRoutine(AudioClip clip)
    {
        yield return ChangeBGMRoutine(1, 0);
        bgmPlayer.clip = clip;
        yield return ChangeBGMRoutine(0, 1);
        bgmPlayer.Play();
    }

    private IEnumerator ChangeBGMRoutine(float start, float end)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime;
            bgmPlayer.volume = Mathf.Lerp(start, end, percent);
            yield return null;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        int playingNum = 1;
        int notPlayingIndex = -1;
        for (int i = 0; i < sfxPlayerList.Count; i++)
        {
            if (sfxPlayerList[i].isPlaying) playingNum++;
            else if (notPlayingIndex == -1) notPlayingIndex = i;
        }

        if (notPlayingIndex == -1)
        {
            AddSFXPlayer();
            notPlayingIndex = sfxPlayerList.Count - 1;
        }

        foreach (AudioSource audioSource in sfxPlayerList)
        {
            if (audioSource.isPlaying)
                audioSource.volume = (0.3f + (0.7f / playingNum));
        }

        sfxPlayerList[notPlayingIndex].volume = (0.3f + (0.7f / playingNum));
        sfxPlayerList[notPlayingIndex].PlayOneShot(clip);
    }

    public void Stop(bool value)
    {
        if (value)
        {
            bgmPlayer.Pause();
            foreach (AudioSource audioSource in sfxPlayerList)
            {
                audioSource.Pause();
            }
        }
        else
        {
            bgmPlayer.UnPause();
            foreach (AudioSource audioSource in sfxPlayerList)
            {
                audioSource.UnPause();
            }
        }
    }
}