using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
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
        //#TODO Move these inputs to the actual player controller, these are just for testing.
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
        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Menu_Main");
        }else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Camera.main.transform.right * -20f);
        }else if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Camera.main.transform.up * 20f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            rb.AddForce(Camera.main.transform.right * 20f);
        }
    }


}
