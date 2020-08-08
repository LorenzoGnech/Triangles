using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IComparable
{
    public List<Card> deck = new List<Card>();
    List<Card> playingDeck;
    public int playerNumber;
    public string playerName;
    public Color playerColor;
    public Transform hand;


    void Start(){

    }

    private void PrintDeck(){
        for (int i = 0; i < playingDeck.Count; i++) {
            Debug.Log(playingDeck[i]);
        }
    }

    public void Setup(string n, int playernum){
        playerName = n;
        playerNumber = playernum;
    }

    void Update()
    {
        
    }

    public void initializeDeck(){
        playingDeck = new List<Card>(deck);
        for (int i = 0; i < playingDeck.Count; i++) {
            Card temp = playingDeck[i];
            int randomIndex = UnityEngine.Random.Range(i, playingDeck.Count);
            playingDeck[i] = playingDeck[randomIndex];
            playingDeck[randomIndex] = temp;
        }
    }

    public void startingDraw(){
        for(int i = 0; i<3; i++){
            drawCard();
        }
    }

    public void drawCard(){
        Debug.Log("Pesco una carta");
        if(playingDeck.Count > 0){
            Card newCard = playingDeck[0];
            Instantiate(newCard, Vector3.zero, Quaternion.Euler(0,0,0), hand);
            playingDeck.RemoveAt(0);
        }
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
