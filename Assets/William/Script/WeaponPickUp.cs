using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] Weapon weapon = null;
    [SerializeField] GameObject DropItem;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
    //    {
    //        other.GetComponent<WeaponSystem>().EquipWeapon(weapon);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                DropItem = other.GetComponent<WeaponSystem>().GetCurrentWeapon().GetDropitemPickUp();
                Instantiate(DropItem, transform.position, Quaternion.identity);

                other.GetComponent<WeaponSystem>().EquipWeapon(weapon);

                Destroy(gameObject);
            }

        }
    }
}
