
using UnityEngine;
using PlayMatch;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class Card : MonoBehaviour, IActions
{

    //Sprites
    [SerializeField] private Sprite _backImage; // Back-facing image of the card
    [SerializeField] private Sprite _frontImage; // Front-facing image of the card

    //Texts
    public TMP_Text _cardIdText;

    //Images
    private Image _cardImage;

    //Buttons
    private Button _cardButton;

    //Variables
    private int _cardID; // Unique identifier for the card
    [SerializeField] private bool _isFlipped = false; // Indicates whether the card is flipped
    private GameController _gameController;

    //Properties
    public int CardId
    {
        get
        {
            return _cardID;
        }
        set
        {
            _cardID = value;
        }
    }
    public Sprite FrontImage
    {
        get
        {
            return _frontImage;
        }
        set
        {
            _frontImage = value;
        }
    }

    private void Awake()
    {
        _cardImage = GetComponent<Image>();
        _cardButton = GetComponent<Button>();
        _gameController = FindObjectOfType<GameController>();

        // Set the initial card image to the back
        _cardImage.sprite = _backImage;

        _cardButton.onClick.AddListener(OnCardClicked);
    }
    public void SetCardText() => _cardIdText.text = _cardID.ToString();

    public void OnCardClicked()
    {
        if (!_isFlipped && _gameController.CanFlip)
        {
            // Flip the card
            _cardImage.sprite = _frontImage;
            _isFlipped = true;

            // Notify the GameController that a card has been flipped
            if (GameController.NotifyCardFlipped != null)
                GameController.NotifyCardFlipped(this);

            if (SoundsView.NotifyPlaySoundClip != null)
                SoundsView.NotifyPlaySoundClip(Constants.flipAudio);

        }
    }
    public void FlipBack()
    {
        // Flip the card back to its initial state (back-facing)
        _cardImage.sprite = _backImage;
        _isFlipped = false;
    }

    public async void DeactivateCard()
    {
        _cardIdText.enabled = false;
        _cardButton.image.CrossFadeAlpha(0, 0.5f, false);
        await WaitTime(500);
    }

    async Task WaitTime(int milliseconds)
    {
        await Task.Delay(milliseconds);
        _cardButton.image.enabled = false;
    }

}

