using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform target;
    [SerializeField] float ProjectileSpeed = 1;
    [SerializeField] float WeaponDamage;

    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * ProjectileSpeed);
    }

    public void SetTarget(Transform target)
    {
        transform.LookAt(target.position);
    }
}
