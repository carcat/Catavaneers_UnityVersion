using UnityEngine;
//Script Write By Will

[CreateAssetMenu(fileName = "Weapon",menuName = "Weapon/Make New Weapon",order = 1)]

public class Weapon : ScriptableObject
{
    //[SerializeField] AnimatorOverrideController animatorOverride;
    [SerializeField] GameObject EquippedPrefab;
    [SerializeField] GameObject DropItemPickUp;
    [SerializeField] float WeaponDamage;
    [SerializeField] float WeaponRange;
    [SerializeField] float AttackSpeed;
    [SerializeField] float Weight;

    const string weaponName = "Weapon";

    public void Spawn(Transform HandTransfrom)
    {
        DestroyOldWeapon(HandTransfrom);
        if(EquippedPrefab != null)
        {
            GameObject NewWeapon = Instantiate(EquippedPrefab, HandTransfrom);
            NewWeapon.name = weaponName;
        }
        
        //if(animatorOverride != null)
        //{
        //    animator.runtimeAnimatorController = animatorOverride;
        //}
    }

    private void SpawnOldWeapon(Vector3 DropLoaction)
    {
        Instantiate(DropItemPickUp, DropLoaction, Quaternion.identity);
    }


    private void DestroyOldWeapon(Transform HandTransfrom)
    {
        Transform oldWeapon = HandTransfrom.Find(weaponName);
        if (oldWeapon == null) return;
        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }

    public GameObject GetDropitemPickUp()
    {
        return DropItemPickUp;
    }

    public float GetDamage()
    {
        return WeaponDamage;
    }

    public float GetWeaponRange()
    {
        return WeaponRange;
    }

    public float GetWeaponAttackSpeed()
    {
        return AttackSpeed;
    }

    public float GetWeaponWeight()
    {
        return Weight;
    }
}