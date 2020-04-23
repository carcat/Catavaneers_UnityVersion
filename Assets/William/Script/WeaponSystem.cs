using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script write by Will

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] float WeaponDemage;
    [SerializeField] Transform Hand;
    public Weapon CurrentWeapon = null;
    [SerializeField] float CurrentWeaponDamage;
    [SerializeField] float CurrentWeaponRange;
    [SerializeField] float CurrentAttackSpeed;
    [SerializeField] float CurrentWeight;
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
        
    }

    public void EquipWeapon(Weapon weapon)
    {
        
        if(CurrentWeapon != null)
        {
            CurrentWeapon = weapon;
        }
        //Animator animator = GetComponent<Animator>();
        
        weapon.Spawn(Hand);

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
