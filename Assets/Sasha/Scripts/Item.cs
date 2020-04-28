using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Default,
        Weapon,
        Trap,
        Consumable
    };
    public string item_name;
    public int item_cost;
    public ItemType type;

}
