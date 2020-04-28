using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] Weapon weapon = null;
    [SerializeField] GameObject DropItem;
    Weapon PlayerOldWeapon;
    bool CanChangeWeapon;

    //private void Update()
    //{
    //    if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0)) && CanChangeWeapon == true)
    //    {
    //        Instantiate(DropItem, transform.position, Quaternion.identity);

    //        other.GetComponent<WeaponSystem>().EquipWeapon(weapon);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.tag == "Player")
    //    {
    //        CanChangeWeapon = true;

    //        if (other.GetComponent<WeaponSystem>().GetCurrentWeapon() != null)
    //        {
    //            DropItem = other.GetComponent<WeaponSystem>().GetCurrentWeapon().GetDropitemPickUp();

    //        }

    //        other.GetComponent<WeaponSystem>().EquipWeapon(weapon);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        CanChangeWeapon = true;
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {

    //        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
    //        {
    //            if (other.GetComponent<WeaponSystem>().GetCurrentWeapon() != null)
    //            {
    //                DropItem = other.GetComponent<WeaponSystem>().GetCurrentWeapon().GetDropitemPickUp();
    //                Instantiate(DropItem, transform.position, Quaternion.identity);
    //            }

    //            other.GetComponent<WeaponSystem>().EquipWeapon(weapon);

    //            Destroy(gameObject);
    //        }

    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "PickUP")
    //    {

    //        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
    //        {
    //            if (other.GetComponent<WeaponSystem>().GetCurrentWeapon() != null)
    //            {
    //                DropItem = other.GetComponent<WeaponSystem>().GetCurrentWeapon().GetDropitemPickUp();
    //                Instantiate(DropItem, transform.position, Quaternion.identity);
    //            }

    //            other.GetComponent<WeaponSystem>().EquipWeapon(weapon);

    //            Destroy(gameObject);
    //        }

    //    }
    //}

    public Weapon GetThisWeapon()
    {
        return weapon;
    }
}

