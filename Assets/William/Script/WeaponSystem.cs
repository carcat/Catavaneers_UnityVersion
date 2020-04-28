using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script write by Will

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] float WeaponDemage;
    [SerializeField] Transform RightHand = null;
    [SerializeField] Transform LeftHand = null;
    [SerializeField] Weapon CurrentWeapon = null;
    [SerializeField] float CurrentWeaponDamage;
    [SerializeField] float CurrentWeaponRange;
    [SerializeField] float CurrentAttackSpeed;
    [SerializeField] float CurrentWeight;
    [SerializeField] Transform ProjectileShootingPoint = null;
    [SerializeField] GameObject WeaponDropLocation;
    [SerializeField] GameObject DropWeapon;
    [SerializeField] Weapon WeaponThatisGoingToEquipt;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(CurrentWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("Menu bar shows up");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(ProjectileShootingPoint == null) { return; }

            //if(CurrentWeapon.HasProjectile())
            //{
            //    CurrentWeapon.LaunchProjectile(RightHand, LeftHand, ProjectileShootingPoint);
            //}
        }

        //if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0)) && WeaponThatisGoingToEquipt != null)
        //{
        //    if (CurrentWeapon != null)
        //    {
        //        Instantiate(CurrentWeapon.GetDropitemPickUp(), WeaponDropLocation.transform.position, Quaternion.identity);
                
        //    }
            
        //    EquipWeapon(WeaponThatisGoingToEquipt);
        //    WeaponThatisGoingToEquipt = null;
        //}
    }

    public void EquipWeapon(Weapon weapon)
    {
        
        if(CurrentWeapon != null)
        {
            CurrentWeapon = weapon;
        }
        //Animator animator = GetComponent<Animator>();
        
        //weapon.Spawn(RightHand, LeftHand);

        CurrentWeaponDamage = weapon.GetDamage();
        CurrentWeaponRange = weapon.GetWeaponRange();
        CurrentAttackSpeed = weapon.GetWeaponAttackSpeed();
        CurrentWeight = weapon.GetWeaponWeight();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        other.gameObject.GetComponent<HealthComp>().TakeDamage(CurrentWeaponDamage);
    //    }

    //    if (other.gameObject.tag == "WeaponPickUP")
    //    {
    //        WeaponThatisGoingToEquipt = other.GetComponent<WeaponPickUp>().GetThisWeapon();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "WeaponPickUP")
    //    {
    //        WeaponThatisGoingToEquipt = null;
    //    }
    //}


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WeaponPickUP" && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            if (CurrentWeapon != null)
            {
                //Instantiate(CurrentWeapon.GetDropitemPickUp(), transform.position, Quaternion.identity);
            }

            EquipWeapon(other.GetComponent<WeaponPickUp>().GetThisWeapon());

            Destroy(other.gameObject);
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.tag == "WeaponPickUP")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
    //        {
    //            if (CurrentWeapon != null)
    //            {
    //                Instantiate(CurrentWeapon.GetDropitemPickUp(), transform.position, Quaternion.identity);
    //            }

    //            EquipWeapon(collision.transform.gameObject.GetComponent<WeaponPickUp>().GetThisWeapon());
    //            Debug.Log("ChangeWeapon");
    //            Destroy(collision.transform.gameObject);
    //        }
    //    }
    //}



    public Weapon GetCurrentWeapon()
    {
        return CurrentWeapon;
    }
}
