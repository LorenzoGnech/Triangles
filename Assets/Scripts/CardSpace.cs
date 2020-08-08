using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpace : MonoBehaviour
{
    public Card card;
    public CardSpace Cbase;
    public CardSpace Cleft;
    public CardSpace Cright;
    private int id;
    
    void Start()
    {
        string prefix = "Spazio_";
        string n = this.name;
        id = int.Parse(n.Replace(prefix, ""));
        bool up = this.tag == "spazio_up";
        if(up){
            int left = id-1;
            int right = id+1;
            int bottom = id+13;
            if(left != 0 && left != 26) Cleft = GameObject.Find(prefix + left).GetComponent<CardSpace>();
            if(right != 14 && right != 40) Cright = GameObject.Find(prefix + right).GetComponent<CardSpace>();
            if(bottom < 52) Cbase = GameObject.Find(prefix + bottom).GetComponent<CardSpace>();
        } else{
            int left = id+1;
            int right = id-1;
            int bottom = id-13;
            if(left != 27 && left != 53)  Cleft = GameObject.Find(prefix + left).GetComponent<CardSpace>();
            if(right != 13 && right != 39) Cright = GameObject.Find(prefix + right).GetComponent<CardSpace>();
            if(bottom > 0) Cbase = GameObject.Find(prefix + bottom).GetComponent<CardSpace>();
        }
    }

    public int getBasePower(){
        if(card == null) return -1;
        return card.baseValue;
    }

    public int getLeftPower(){
        if(card == null) return -1;
        return card.leftValue;
    }

    public int getRightPower(){
        if(card == null) return -1;
        return card.rightValue;
    }

    public void executeAttack(){
        if(Cbase != null && Cbase.card != null && card.owner != Cbase.card.owner && attack(getBasePower(), Cbase.getBasePower())){
            if(Cbase.conquer(card.owner)){
                Cbase.executeAttack();
            }
        }
        if(Cleft != null && Cleft.card != null && card.owner != Cleft.card.owner && attack(getLeftPower(), Cleft.getLeftPower())){
            if(Cleft.conquer(card.owner)){
                Cleft.executeAttack();
            }

        }
        if(Cright != null && Cright.card != null && card.owner != Cright.card.owner && attack(getRightPower(), Cright.getRightPower())){
            if(Cright.conquer(card.owner)){
                Cright.executeAttack();
            }
        }
    }

    public bool conquer(Player conqueror){
        //SE NON SI PUO' CONQUISTARE RITORNA FALSO
        card.owner = conqueror;
        card.img.color = conqueror.playerColor;
        //card.updateRender();
        return true;
    }

    private bool attack(int attack, int defense){
        return attack > defense;
    }

    void Update()
    {
        
    }
}
