using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int numberOfPlayers = 2;
    public int turnoAttuale;
    public GameObject canvas;
    public GameObject descrizioneCarta;
    List<Player> players = new List<Player>();

    void Awake()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in temp){
            players.Add(g.GetComponent<Player>());
        }
        players.Sort();
        turnoAttuale = 1;
        foreach(Player p in players){
            p.initializeDeck();
            p.startingDraw();
        }
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
                c.disattiva();
            }
        }
    }

    void RimuoviBlocchi(Card[] cards){
        foreach(Card c in cards){
            c.attiva();
        }
    }

    public void execute(Card attacker){
        CardSpace cSpace = attacker.transform.parent.GetComponent<CardSpace>();
        cSpace.executeAttack();
    }

    public void MostraDescrizione(Card card){
        descrizioneCarta.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = card.sprite;
        GameObject sp_effects = descrizioneCarta.transform.GetChild(1).gameObject;
        GameObject stats = descrizioneCarta.transform.GetChild(2).gameObject;
        descrizioneCarta.transform.GetChild(3).gameObject.GetComponent<Text>().text = card.effect_desc;
        descrizioneCarta.transform.GetChild(4).gameObject.GetComponent<Text>().text = card.lore;
        string sp_effects_text = card.desc_SP1 + "\n\n" + card.desc_SP2 + "\n\n" + card.desc_SP3;
        sp_effects.GetComponent<Text>().text = sp_effects_text;
        stats.transform.GetChild(0).gameObject.GetComponent<Text>().text = card.baseValue.ToString();
        stats.transform.GetChild(1).gameObject.GetComponent<Text>().text = card.leftValue.ToString();
        stats.transform.GetChild(2).gameObject.GetComponent<Text>().text = card.rightValue.ToString();
        descrizioneCarta.SetActive(true);
    }

    public void NascondiDescrizione(){
        descrizioneCarta.SetActive(false);
    }
}
