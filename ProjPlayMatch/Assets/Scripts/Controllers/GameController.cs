using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMatch;

public class GameController : MonoBehaviour, IActionables
{
    //Lists
    private List<Card> flippedCards = new List<Card>();

    public void InitializeGame()
    {

    }
    public bool CanFlip { get { return flippedCards.Count < 2; } }

    public IEnumerator FlipAllCardsBack(List<Card> cards)
    {
        yield return new WaitForSeconds(1.0f);
    }

    public void OnCardFlipped(Card card)
    {

    }

    public IEnumerator FlipCardsBack()
    {
        yield return new WaitForSeconds(0.0f);
    }
}


