using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData.asset", menuName = "Triangles/Cards/CardData", order = 0)]
public class CardData : ScriptableObject {
    public string cardName;
    public int baseV;
    public int leftV;
    public int rightV;
    public int id_SP1;
    public int id_SP2;
    public int id_SP3;
    public int id_effect;
    public string effect_description;
    public string lore;
    public Sprite artwork;
}
