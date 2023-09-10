using PlayMatch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicView : MonoBehaviour
{
    //Actions
    public static Action<bool> NotifyMusicState;

    //Audiosource
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        NotifyMusicState += OnMusicState;
    }

    private void OnMusicState(bool state)
    {
        switch (state)
        {
            case true:
                {
                    _audioSource.Play();
                }
                break;
            case false:
                {
                    _audioSource.Pause();
                }
                break;
        }
    }
    private void OnDisable()
    {
        NotifyMusicState -= OnMusicState;
    }

}
