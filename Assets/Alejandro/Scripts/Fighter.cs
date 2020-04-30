using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] float CharacterSpeed = 0.0f;
    [SerializeField] float CharacterAttackSpeed = 0.0f;
    [SerializeField] float CharacterAttackDamage = 0.0f;
    [SerializeField] Weapon defaultWeapon = null;
    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] Weapon currentWeapon;

    HealthComp target;
    float timeSinceLastAttack = Mathf.Infinity;
    void Start()
    {
        if (currentWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
        else
        {
            EquipWeapon(currentWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if(Input.GetButton("Submit/Interact") && timeSinceLastAttack > GetCurrentAttackSpeed())
        {
            timeSinceLastAttack = 0;
            GetComponent<Animator>().SetTrigger("Attack");
        }
        if (Input.GetButtonDown("Dodge"))
        {
            GetComponent<Animator>().SetTrigger("Roll");
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        GetComponent<PlayerController>().SetWeaponWeight(currentWeapon.GetWeaponWeight());
        Animator animator = GetComponent<Animator>();
        weapon.Spawn(rightHandTransform, leftHandTransform, animator);
    }

    float GetCurrentAttackSpeed()
    {
        return CharacterAttackSpeed * currentWeapon.GetWeaponAttackSpeed();
    }
    float GetCurrentAttackDamage()
    {
        return CharacterAttackDamage * currentWeapon.GetDamage();
    }
    float GetCurrentCharacterSpeed()
    {
        return CharacterSpeed * currentWeapon.GetWeaponWeight();
    }
}
