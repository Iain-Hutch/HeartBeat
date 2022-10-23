using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// component used to loop an audioclip from an AudioSource component

public class PlaySoundContinuous : MonoBehaviour
{
    // clip to be played
    [SerializeField] private AudioClip sound = null;
    // component that alllows for clip activation
    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private float volume = 1.0f;

    // indicates whether the looping clip is currently active
    private bool isPlaying;

    void Awake()
    {
        audioSource.volume = volume;
    }

    public void Play()
    {
        Debug.Log(string.Format("Playing continuous sound '{0}'", sound));
        // set the new audio clip if changed
        audioSource.clip = sound;
        audioSource.Play();
    }

    public void Stop()
    {
        // remove audioclip and pause source
        audioSource.Pause();
        audioSource.clip = null;
    }

    public void TogglePlay()
    {
        bool playToggle = !IsPlaying();
        SetPlay(playToggle);
    }

    public void SetPlay(bool setPlay)
    {
        if (setPlay)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void SetClip(AudioClip newSound)
    {
        sound = newSound;
    }
}
