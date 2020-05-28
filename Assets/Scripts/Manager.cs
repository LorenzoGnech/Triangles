using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int numberOfPlayers = 2;
    public int turnoAttuale;
    public GameObject canvas;
    List<Player> players = new List<Player>();

    void Awake()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in temp){
            players.Add(g.GetComponent<Player>());
        }
        players.Sort();
        turnoAttuale = 1;
        changeTurn();
    }
    public void changeTurn(){
        Card[] cards = canvas.GetComponentsInChildren<Card>();
        RimuoviBlocchi(cards);
        BloccaAvversari(cards);
        Debug.Log("Cambio turno");
        turnoAttuale++;
        if(turnoAttuale > numberOfPlayers){
            turnoAttuale = 1;
        }
    }

    void BloccaAvversari(Card[] cards){
        foreach(Card c in cards){
            if(c.owner.playerNumber != turnoAttuale){
                //Debug.Log("Disattivo " + c);
                c.disattiva();
            }
        }
    }

    void RimuoviBlocchi(Card[] cards){
        foreach(Card c in cards){
            c.attiva();
        }
    }
}
