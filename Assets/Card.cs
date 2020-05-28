using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData cardData;
    Draggable draggable;
    int baseValue;
    int leftValue;
    int rightValue;
    public Player owner;

    void Awake(){
        baseValue = cardData.values[0];
        leftValue = cardData.values[1];
        rightValue = cardData.values[2];
        draggable = GetComponent<Draggable>();
    }

    public void UpdateRotation(){
        int t1 = leftValue;
        int t2 = rightValue;
        rightValue = baseValue;
        leftValue = t2;
        baseValue = t1;
    }

    public void stampaValori(){
        Debug.Log("base: " + baseValue + " - left: " + leftValue + " - right: " + rightValue);
    }

    public void disattiva(){
        draggable.enabled = false;
    }

    public void attiva(){
        draggable.enabled = true;
    }
}
