using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData.asset", menuName = "Triangles/Cards/CardData", order = 0)]
public class CardData : ScriptableObject {
    public string cardType;
    public int[] values = new int[3];
}
