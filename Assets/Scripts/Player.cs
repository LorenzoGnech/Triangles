using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IComparable
{
    List<Card> deck = new List<Card>();
    public int playerNumber;
    public string playerName;
    public Color playerColor;

    public void Setup(string n, int playernum){
        playerName = n;
        playerNumber = playernum;
    }

    void Update()
    {
        
    }

    public int CompareTo(object obj) {
        if (obj == null) return -1;
        
        if(this.playerNumber > ((Player)obj).playerNumber){
            return -1;
        } else{
            return 1;
        }
    }
}
