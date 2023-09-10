using UnityEngine;
using PlayMatch;
using System;

[RequireComponent(typeof(AudioSource))]
public class SoundsView : MonoBehaviour, ISounds
{
    //Actions
    public static Action<string> NotifyPlaySoundClip;
    public static Action<bool> NotifySoundState;

    //Audiosource
    private AudioSource _audioSource;

    //Audio Clips
    [SerializeField] private AudioClip _flipAudio;
    [SerializeField] private AudioClip _matchAudio;
    [SerializeField] private AudioClip _mismatchAudio;
    [SerializeField] private AudioClip _completedAudio;

    //Variables
    private bool _isSoundOn = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        NotifyPlaySoundClip += PlaySound;
        NotifySoundState += OnSoundState;
    }

    public void PlaySound(string clip)
    {
        if (_isSoundOn == false)
            return;

        switch (clip)
        {
            case Constants.flipAudio:
                {
                    _audioSource.PlayOneShot(_flipAudio);
                }
                break;
            case Constants.matchedAudio:
                {
                    _audioSource.PlayOneShot(_matchAudio);
                }
                break;
            case Constants.mismatchedAudio:
                {
                    _audioSource.PlayOneShot(_mismatchAudio);
                }
                break;
            case Constants.completedAudio:
                {
                    _audioSource.PlayOneShot(_completedAudio);
                }
                break;
        }
    }

    private void OnSoundState(bool state)
    {
        switch (state)
        {
            case true:
                {
                    _isSoundOn = true;
                }
                break;
            case false:
                {
                    _isSoundOn = false;
                }
                break;
        }
    }

    private void OnDisable()
    {
        NotifyPlaySoundClip -= PlaySound;
        NotifySoundState -= OnSoundState;
    }
}
