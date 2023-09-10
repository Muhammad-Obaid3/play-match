
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayMatch;
using TMPro;

public class GameView : MonoBehaviour
{

    //Actions
    public static Action<int> NotifyUpdateScore;
    public static Action NotifyCompleted;

    //Buttons
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnHome;
    [SerializeField] private Button _btnSettings;

    //Texts
    public TMP_Text _scoreText; // Text to display the score

    //GameObjects
    [SerializeField] private GameObject _gameOverUIPanel;
    [SerializeField] private GameObject _settingsUIPanel;

    // Start is called before the first frame update
    void Start()
    {
        Utility.SetActive(_gameOverUIPanel, false);
        Utility.SetActive(_settingsUIPanel, false);

        NotifyUpdateScore += UpdateScoreDisplay;
        NotifyCompleted += OnCompleted;

        _btnRestart.onClick.AddListener(() => OnRestart());
        _btnHome.onClick.AddListener(() => OnHome());
        _btnSettings.onClick.AddListener(() => OnSettings());

    }

    private void OnSettings()
    {
        Utility.SetActive(_settingsUIPanel, true);
    }

    private void OnCompleted()
    {
        Utility.SetActive(_gameOverUIPanel, true);
    }

    private void UpdateScoreDisplay(int score)
    {
        _scoreText.text = $"SCORE : {score}";
    }
    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnHome()
    {
        SceneManager.LoadSceneAsync(Constants.loadingSceneIndex);
    }

    private void OnDisable()
    {
        NotifyUpdateScore -= UpdateScoreDisplay;
        NotifyCompleted -= OnCompleted;
    }
}


