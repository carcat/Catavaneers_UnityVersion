using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int gold=1000;
    public Item WeaponItem;
    public Item ConsumableItem;
    public Item TrapItem;
    public ShopPlot plotref;
    // Start is called before the first frame update


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (plotref)
            {
                plotref.CheckIfCanPurchase();
            }
            else
            {
                Debug.Log("Tried to purchase but no plot reference");
            }
        }else if (Input.GetKeyDown(KeyCode.J))
        {
            if (WeaponItem)
            {
                Debug.Log("Use Weapon");
                Destroy(WeaponItem.gameObject);
            }
            else
            {
                Debug.Log("No Weapon in inventory");
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (ConsumableItem)
            {
                Debug.Log("Use Consumable");
                Destroy(ConsumableItem.gameObject);
            }
            else
            {
                Debug.Log("No Consumable in inventory");
            }
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (TrapItem)
            {
                Debug.Log("Use Trap");
                Destroy(TrapItem.gameObject);
            }
            else
            {
                Debug.Log("No trap in inventory");
            }
        }
    }


}
