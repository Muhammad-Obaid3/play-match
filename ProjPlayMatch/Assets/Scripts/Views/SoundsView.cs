using UnityEngine;
using PlayMatch;
using System;

[RequireComponent(typeof(AudioSource))]
public class SoundsView : MonoBehaviour, ISounds
{
    //Actions
    public static Action<string> NotifyPlaySoundClip;
    //Audiosource
    private AudioSource _audioSource;

    //Audio Clips
    [SerializeField] private AudioClip _flipAudio;
    [SerializeField] private AudioClip _matchAudio;
    [SerializeField] private AudioClip _mismatchAudio;
    [SerializeField] private AudioClip _completedAudio;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        NotifyPlaySoundClip += PlaySound;
    }

    public void PlaySound(string clip)
    {
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

    public void StopSound()
    {

    }

    private void OnDisable()
    {
        NotifyPlaySoundClip -= PlaySound;
    }
}
