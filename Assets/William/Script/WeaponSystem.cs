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

            if(CurrentWeapon.HasProjectile())
            {
                CurrentWeapon.LaunchProjectile(RightHand, LeftHand, ProjectileShootingPoint);
            }
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        
        if(CurrentWeapon != null)
        {
            CurrentWeapon = weapon;
        }
        //Animator animator = GetComponent<Animator>();
        
        weapon.Spawn(RightHand, LeftHand);

        CurrentWeaponDamage = weapon.GetDamage();
        CurrentWeaponRange = weapon.GetWeaponRange();
        CurrentAttackSpeed = weapon.GetWeaponAttackSpeed();
        CurrentWeight = weapon.GetWeaponWeight();
    }

    public Weapon GetCurrentWeapon()
    {
        return CurrentWeapon;
    }
}
