using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMatch;
using System;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour, IActionables, IAnimations
{
    //Actions
    public static Action<Card> NotifyCardFlipped;

    //Lists
    private List<Card> _flippedCards = new List<Card>();
    private List<Card> _cards = new List<Card>();
    private List<int> _shuffledIDs = new List<int>();

    //Transforms
    public Transform _parentTransform;

    //GameObjects
    public GameObject _cardPrefab; // Prefab of the card object

    //Sprites
    [Header("This must be equal to gridSize x gridSize")]
    public Sprite[] _cardImages; // Array of card images (front-facing)

    //Variables
    [SerializeField] private int _gridSize = 4;
    private int _score = 0;
    private int _currentCombo = 0;
    private int _highestCombo = 0;
    private int _comboMultiplier = 1;
    private int _matchedPairs = 0;

    //Properties
    public bool CanFlip { get { return _flippedCards.Count < 2; } }


    private void Start()
    {
        InitializeGame();
        NotifyCardFlipped += OnCardFlipped;
    }


    public void InitializeGame()
    {
        try
        {
            int totalPairs = _gridSize * _gridSize / 2;

            // Create a list of card IDs in pairs
            for (int i = 0; i < totalPairs; i++)
            {
                _shuffledIDs.Add(i);
                _shuffledIDs.Add(i);
            }

            // Shuffle the card IDs randomly
            for (int i = 0; i < _shuffledIDs.Count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(i, _shuffledIDs.Count);
                int temp = _shuffledIDs[i];
                _shuffledIDs[i] = _shuffledIDs[randomIndex];
                _shuffledIDs[randomIndex] = temp;
            }

            // Shuffle the cards
            //Shuffle(cards);

            for (int i = 0; i < _gridSize * _gridSize; i++)
            {
                int cardIndex = i;
                GameObject cardGO = Instantiate(_cardPrefab, _parentTransform);
                cardGO.SetActive(true);
                Card card = cardGO.GetComponent<Card>();

                // Set the card's ID and other properties as needed
                card.CardId = _shuffledIDs[cardIndex];
                card.SetCardText();
                card.FrontImage = _cardImages[_shuffledIDs[cardIndex]];
                _cards.Add(card);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"{ex} <b> ::Resolution: Please add card images as gridSize x gridSize </b>");
        }

        // Temporarily flip all cards to reveal their faces
        foreach (var card in _cards)
        {
            card.OnCardClicked(); // Simulate a click on each card to reveal its face
        }

        // Delay for 1 second before flipping the cards back
        StartCoroutine(FlipAllCardsBack(_cards));
    }
    void Shuffle(List<Card> cards)
    {
        //int count = cards.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    int randomIndex = Random.Range(i, count);
        //    Card temp = cards[i];
        //    cards[i] = cards[randomIndex];
        //    cards[randomIndex] = temp;
        //}

    }
    public IEnumerator FlipAllCardsBack(List<Card> cards)
    {
        yield return new WaitForSeconds(1.0f);

        foreach (var card in cards)
        {
            card.FlipBack();
        }
    }

    public void OnCardFlipped(Card card)
    {
        _flippedCards.Add(card);

        if (_flippedCards.Count == 2)
        {
            // Check if the two flipped cards match
            if (_flippedCards[0].CardId == _flippedCards[1].CardId)
            {
                // Match found
                _score += _comboMultiplier; // Increase the score based on the combo multiplier

                if (GameView.NotifyUpdateScore != null)
                    GameView.NotifyUpdateScore(_score);
                // Increase the combo
                _currentCombo++;
                if (_currentCombo > _highestCombo)
                {
                    _highestCombo = _currentCombo;
                }

                Debug.Log("_currentCombo" + _currentCombo);
                Debug.Log("_highestCombo" + _highestCombo);

                // Update the combo multiplier (customize this logic as needed)
                //The combo multiplier increases every two consecutive matches,
                //capped at a maximum value of 5(customize this logic as needed).
                _comboMultiplier = Mathf.Min(_currentCombo / 2 + 1, 5);
                _flippedCards.ForEach(cards => cards.DeactivateCard());
                if (SoundsView.NotifyPlaySoundClip != null)
                    SoundsView.NotifyPlaySoundClip(Constants.matchedAudio);
                _matchedPairs++;

                if (_matchedPairs == _gridSize * _gridSize / 2)
                {
                    if (GameView.NotifyCompleted != null)
                        GameView.NotifyCompleted();
                    if (SoundsView.NotifyPlaySoundClip != null)
                        SoundsView.NotifyPlaySoundClip(Constants.completedAudio);
                }

                _flippedCards.Clear();
            }
            else
            {
                // No match found, reset the combo
                _currentCombo = 0;
                _comboMultiplier = 1;
                // No match found, flip the cards back
                StartCoroutine(FlipCardsBack());
                if (SoundsView.NotifyPlaySoundClip != null)
                    SoundsView.NotifyPlaySoundClip(Constants.mismatchedAudio);
            }
        }
    }

    public IEnumerator FlipCardsBack()
    {
        yield return new WaitForSeconds(0.0f);

        foreach (var card in _flippedCards)
        {
            card.FlipBack();
        }

        _flippedCards.Clear();
    }

    public void PlayAnimation()
    {
    }

    public void StopAnimation()
    {
    }

    private void OnDisable()
    {
        NotifyCardFlipped -= OnCardFlipped;
        StopAllCoroutines();
    }
}


