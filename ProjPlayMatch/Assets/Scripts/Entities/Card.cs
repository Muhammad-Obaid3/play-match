using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMatch;
using System;
using UnityEngine.UI;

public class Card : MonoBehaviour, IActions
{

    //Variables
    public int cardID; // Unique identifier for the card
    public bool isFlipped = false; // Indicates whether the card is flipped


    public void OnCardClicked()
    {
        //card  clicked
    }
    public void FlipBack()
    {
        // Flip the card back to its initial state (back-facing)

    }
    public void DeactivateCard()
    {
        //handle card deactivation
    }
    public void ActivateCard()
    {
        //handle card activation
    }



}

