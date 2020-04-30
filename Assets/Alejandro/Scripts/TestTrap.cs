using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestTrap : MonoBehaviour
{
    public enum TrapType
    {
        Freeze,
        Reverse,
        Slow
    }

    [SerializeField] TrapType type;
    [SerializeField] float aflictionValue = 0.0f;
    [SerializeField] float duration = 1;
    PlayerController target;
    int reverse = 1;
    float slow = 1;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player in trap = " + type);
            target = other.GetComponent<PlayerController>();
            if (type == TrapType.Freeze) FreezeTrap(true);
            if (type == TrapType.Reverse) ReverseTrap(false, aflictionValue);
            if (type == TrapType.Slow) SlowTrap(false, aflictionValue);
            Destroy(gameObject);
        }
    }
    private void SlowTrap(bool freeze, float slow)
    {
        target.HitByTrap(freeze, reverse, slow, duration);
    }

    private void ReverseTrap(bool freeze, float reverse)
    {
        target.HitByTrap(freeze, reverse, slow, duration);
    }

    private void FreezeTrap(bool freeze)
    {
        target.HitByTrap(freeze, reverse, slow, duration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
