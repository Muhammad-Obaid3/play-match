
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayMatch;
using UnityEngine.SceneManagement;

public class MainView : MonoBehaviour
{
    //Images
    [SerializeField] private Image _filledImage;

    //UI GameObjects
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _mainUIPanel;
    [SerializeField] private GameObject _settingsUIPanel;

    //Buttons
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnQuit;

    //Variables
    [SerializeField] private float _waitingTime = 5.0f;


    private void Start()
    {
        _btnSettings.onClick.AddListener(() => OnSettings());
        _btnPlay.onClick.AddListener(() => OnPlay());
        _btnQuit.onClick.AddListener(() => OnQuit());

        Utility.SetActive(_loadingPanel, true);
        Utility.SetActive(_mainUIPanel, false);
        Utility.SetActive(_settingsUIPanel, false);

        StartCoroutine(LoadGameScene());
    }

    private void Update()
    {
        _filledImage.fillAmount += 1 / _waitingTime * Time.deltaTime;
    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitUntil(() => _filledImage.fillAmount >= 1);
        Utility.SetActive(_loadingPanel, false);
        Utility.SetActive(_mainUIPanel, true);
    }
    private void OnPlay()
    {
        SceneManager.LoadSceneAsync(Constants.gameSceneIndex);
    }
    private void OnSettings()
    {
        Utility.SetActive(_settingsUIPanel, true);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

}


