using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMatch
{
    public interface IActionables
    {
        void InitializeGame();
        bool CanFlip { get; }
        IEnumerator FlipAllCardsBack(List<Card> cards);
        void OnCardFlipped(Card card);
        IEnumerator FlipCardsBack();

    }

}
