using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItem", menuName = "ScriptableObjects/CollectableItem", order = 1)]
public class CollectableItem : ScriptableObject
{
    public string ItemName;
    public float Weight;
    public int Value;
    public string Description;
    public ItemCategory ItemCategory; 
}

public enum ItemCategory
{
    All,
    Food,
    Weapon,
    Potion,

    LengthOfEnum
}
