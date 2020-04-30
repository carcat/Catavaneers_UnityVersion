using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour //Sasha
{
    public int gold=1000;
    public Item WeaponItem;
    public Item ConsumableItem;
    public Item TrapItem;
    public ShopPlot plotref;
    Rigidbody rb;
    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (WeaponItem)
        {
            GetComponent<Fighter>().EquipWeapon(WeaponItem.WeaponRef);
            WeaponItem = null;
        }
        //#TODO Move these inputs to the actual player controller
        if (Input.GetButtonDown("Buy"))
        {
            if (plotref)
            {
                plotref.CheckIfCanPurchase();
            }
            else
            {
                Debug.Log("Tried to purchase but no plot reference");
            }
        }else if (Input.GetButtonDown("Use Item"))
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
        
        else if (Input.GetButtonDown("Cancel/Shop3"))
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
        /*   Test Controller in shop test scene
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Camera.main.transform.right * -20f);
        }else if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Camera.main.transform.up * 20f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            rb.AddForce(Camera.main.transform.right * 20f);
        }*/
    }


}
