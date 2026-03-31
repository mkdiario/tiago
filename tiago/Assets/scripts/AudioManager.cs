using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{
    public static AudioManager Instance;
 
    private AudioSource systemSource;
    private List<AudioSource> activeSources;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            systemSource = GetComponent<AudioSource>();
            activeSources = new List<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //2d
    public void Play(AudioClip clip)
    {
        systemSource.Stop();
        systemSource.clip = clip;
        systemSource.Play();
    }
    public void Stop()
    {
        systemSource.Stop();
    }
    public void Pause()
    {
        systemSource.Pause();
    }
 
    public void Resume()
    {
        systemSource.UnPause();
    }
    public void PlayOneShot(AudioClip clip)
    {
        systemSource.PlayOneShot(clip);
    }
    //3d
    public void Play(AudioClip clip, AudioSource source)
    {
        if (activeSources.Contains(source))
            activeSources.Add(source);
        systemSource.Stop();
        systemSource.clip = clip;
        systemSource.Play();
    }
    public void Stop(AudioSource source)
    {
        if(activeSources.Contains(systemSource))
            activeSources.Remove(systemSource);
        systemSource.Stop();
    }
    public void Pause(AudioSource source)
    {
        source.Pause();
    }
 
    public void Resume(AudioSource source)
    {
        source.UnPause();
    }

    public void playoneshot(AudioSource source)
    {
        source.playOnAwake = false;
    }
}