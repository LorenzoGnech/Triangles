using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;
    Draggable draggable;
    public int baseValue;
    public int leftValue;
    public int rightValue;
    public int[] sp_effects = new int[3];
    public int effect;
    public Sprite sprite;
    public string effect_desc;
    public string lore;
    public string desc_SP1;
    public string desc_SP2;
    public string desc_SP3;
    public Player owner;
    public Image img;

    void Awake(){
        baseValue = cardData.baseV;
        leftValue = cardData.leftV;
        rightValue = cardData.rightV;
        sp_effects[0] = cardData.id_SP1;
        sp_effects[1] = cardData.id_SP2;
        sp_effects[2] = cardData.id_SP3;
        effect = cardData.id_effect;
        effect_desc = cardData.effect_description;
        sprite = cardData.artwork;
        lore = cardData.lore;
        desc_SP1 = "";
        desc_SP2 = "";
        desc_SP3 = "";
        draggable = GetComponent<Draggable>();
        img.color = owner.playerColor;
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
