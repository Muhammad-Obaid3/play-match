using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMatch;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour, ISettings
{

    //Buttons
    [SerializeField] private Button _btnSoundOn;
    [SerializeField] private Button _btnSoundOff;
    [SerializeField] private Button _btnMusicOn;
    [SerializeField] private Button _btnMusicOff;

    [SerializeField] private Button _btnClose;


    //Variables
    private bool _isSound = true;
    private bool _isMusic = true;

    private void Start()
    {
        _btnSoundOn.onClick.AddListener(() => OnSound());
        _btnSoundOff.onClick.AddListener(() => OnSound());
        _btnMusicOn.onClick.AddListener(() => OnMusic());
        _btnMusicOff.onClick.AddListener(() => OnMusic());

        _btnClose.onClick.AddListener(() => OnClose());
    }

    public void OnClose()
    {
        Utility.SetActive(this.gameObject, false);
    }

    public void OnMusic()
    {
        _isMusic = !_isMusic;
        switch (_isMusic)
        {
            case true:
                {
                    Utility.SetActive(_btnMusicOn.gameObject, true);
                    Utility.SetActive(_btnMusicOff.gameObject, false);
                }
                break;
            case false:
                {
                    Utility.SetActive(_btnMusicOn.gameObject, false);
                    Utility.SetActive(_btnMusicOff.gameObject, true);
                }
                break;
        }

        MusicView.NotifyMusicState(_isMusic);

    }

    public void OnSound()
    {
        _isSound = !_isSound;
        switch (_isSound)
        {
            case true:
                {
                    Utility.SetActive(_btnSoundOn.gameObject, true);
                    Utility.SetActive(_btnSoundOff.gameObject, false);
                }
                break;
            case false:
                {
                    Utility.SetActive(_btnSoundOn.gameObject, false);
                    Utility.SetActive(_btnSoundOff.gameObject, true);
                }
                break;
        }
        SoundsView.NotifySoundState(_isSound);
    }
}
