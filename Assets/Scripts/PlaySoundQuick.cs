using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundQuick : MonoBehaviour
{
    // clip to be played
    [SerializeField] private AudioClip sound = null;
    // component that alllows for clip activation
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private List<AudioClip> soundList = null;

    [SerializeField] private float volume = 1.0f;
    [SerializeField] private float pitch = 1.0f;

    [SerializeField] private bool variance = false;
    [SerializeField] private float pitchVariance = 0.0f;
    
    void Awake()
    {
        audioSource.volume = volume;
    }

    public void PlayFromList(int index)
    {
        sound = soundList[index];
        Play();
    }

    public void PlayFromListRandom()
    {
        SetSoundFromListRandom();
        Debug.Log(string.Format("setting sound random: '{0}'", sound));
        Play();
    }


    public void SetSoundFromListRandom()
    {
        // get a randomised index and check if it is the currently-set clip
        int index = Random.Range(0, soundList.Count);
        AudioClip newClip = soundList[index];
        // ensure that no clip is repeated
        while (newClip == sound)
        {
            index = Random.Range(0, soundList.Count);
            newClip = soundList[index];
        }

        sound = newClip;
    }

    public void ResetList(List<AudioClip> clips)
    {
        soundList.Clear();
        foreach (AudioClip clip in clips)
            soundList.Add(clip);
    }

    public void Play()
    {
        Debug.Log(string.Format("Playing quick sound '{0}'", this));
        // set the new audio clip if changed
        audioSource.clip = sound;
        float tempPitch = pitch;
        if (variance)
        {
            float rangedVariance = Random.Range(-pitchVariance, pitchVariance);
            tempPitch += rangedVariance;
        }

        audioSource.pitch = tempPitch;
        Debug.Log(string.Format("Playing '{0}' at volume '{1}'", sound, volume));
        audioSource.PlayOneShot(sound, volume);
        // reset audiosource pitch
        audioSource.pitch = pitch;
    }

    public void SetClip(AudioClip newSound)
    {
        sound = newSound;
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }

    public void SetVariance(float var)
    {
        variance = true;
        pitchVariance = var;
    }

    public void SetMix(float mix)
    {
        audioSource.reverbZoneMix = mix;
    }
}
