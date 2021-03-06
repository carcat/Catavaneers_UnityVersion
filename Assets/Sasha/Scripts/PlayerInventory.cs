﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour //Sasha
{
    public SoundClipsInts soundCue = SoundClipsInts.Buying;
    public string playername;
    public int kills=0;
    public int gold=1000;
    public Item WeaponItem;
    public Item ConsumableItem;
    public Item TrapItem;
    public ShopPlot plotref;
    Rigidbody rb;
    public HealthComp health;

    //ui
    public Text NameUI;
    public Image WeaponUI;
    public Image Trap1UI;
    public Image Trap2UI;
    public Image ConsumableUI;
    public Text GoldUI;
    public Text HealthUI;
    public Text KillsUI;
  
    // Start is called before the first frame update

    private void Start()
    {
        health = GetComponent<HealthComp>();
        rb = GetComponent<Rigidbody>();
    }

   
    private void Update()
    {
        RefreshUI();

        //#TODO Move these inputs to the actual player controller
        if (Input.GetButtonDown("Buy"))
        {
            if (plotref)
            {
                plotref.CheckIfCanPurchase();
           
                if (TrapItem)
                {
                    GetComponent<TrapSystem>().EquipTrap(TrapItem.TrapRef);
                    int trapno = GetComponent<TrapSystem>().CheckHasTrap();

                    switch (trapno)
                    {
                        case 0:
                            Debug.Log("no traps");
                            MusicManager.Instance.PlaySoundTrack(soundCue);
                            break;
                        case 1:
                            Debug.Log("Trap1");
                            Trap1UI.sprite = TrapItem.Item_Display;
                            MusicManager.Instance.PlaySoundTrack(soundCue);
                            break;
                        case 2:
                            Debug.Log("Only Trap 2");
                            MusicManager.Instance.PlaySoundTrack(soundCue);
                            break;
                        case 3:
                            Debug.Log("both traps");
                            Trap2UI.sprite = TrapItem.Item_Display;
                            break;
                    }
                    
                    TrapItem = null;
                    //Destroy(TrapItem);
                }
                if (WeaponItem)
                {
                
                    GetComponent<Fighter>().EquipWeapon(WeaponItem.WeaponRef);
                    WeaponUI.sprite = WeaponItem.Item_Display;
                    WeaponItem = null;
                    MusicManager.Instance.PlaySoundTrack(soundCue);

                    //Destroy(WeaponItem);
                }
                if (ConsumableItem)
                {
                    ConsumableUI.sprite = ConsumableItem.Item_Display;
                    MusicManager.Instance.PlaySoundTrack(soundCue);
                }
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
                GetComponent<HealthComp>().AddHealth(25);
                ConsumableUI.sprite = null;
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
                int trapno = GetComponent<TrapSystem>().CheckHasTrap();
                Debug.Log("Use Trap");
                switch (trapno)
                {
                    case 0:
                        Debug.Log("no traps");
                        Trap1UI = null;
                        break;
                    case 1:
                        Debug.Log("Trap1");
                        Trap2UI = null;

                        break;
                    case 2:
                        Debug.Log("Only Trap 2");
                        break;
                    case 3:
                        Debug.Log("both traps");
                       
                        break;
                }
               
                
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
    public void RefreshUI()
    {
        //#TODO Make these not in update
        GoldUI.text = gold.ToString();
        KillsUI.text = kills.ToString();
        HealthUI.text = health.GetCurHealth().ToString();
        NameUI.text = playername;
    }


}
