﻿using AI;
using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{    
    public enum TrapType
    {
        None,
        Freeze,
        Reverse,
        Slow, 
        Damage
    }

    [SerializeField] TrapType type = TrapType.None;
    [SerializeField] [Range(0f, 1f)] float SpeedModifier;
    [SerializeField] float aflictionValue = 0.0f;
    [SerializeField] float duration = 1;
    [SerializeField] int UsageLeft;
    PlayerController target;
    int reverse = 1;
    float slow = 1;

    //below is edit by Will
    [SerializeField]float ActivateTimer = 0;
    float CurrentTime;
    [SerializeField] int TrapDamage = 5;
    [SerializeField] bool AreaEffect = false;
    private float AreaEffectRadius =5f;
    private void Start()
    {
        CurrentTime = ActivateTimer;
    }

    private void Update()
    {
        if (CurrentTime > 0)
            CurrentTime -= 1 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {


        if (AreaEffect == false & other.tag == "Player" & CurrentTime <= 0f)
        {
            Debug.Log("trap Activate");
            CurrentTime += 3;
            UsageLeft--;
            Debug.Log("Player in trap = " + type);
            target = other.GetComponent<PlayerController>();
            if (type == TrapType.Freeze) FreezeTrap();
            if (type == TrapType.Reverse) ReverseTrap(aflictionValue);
            if (type == TrapType.Slow) SlowTrap(aflictionValue);
            if (type == TrapType.Damage) other.GetComponent<HealthComp>().TakeDamage(TrapDamage);         
        }

        if (AreaEffect == false && other.tag == "Enemy" & CurrentTime <= 0f)
        {
            Debug.Log("trap Activate");
            CurrentTime += 3;
            UsageLeft--;
            Controller TempEnemyController = other.GetComponent<Controller>();
            if (type == TrapType.Damage) other.GetComponent<HealthComp>().TakeDamage(TrapDamage);
            if (type == TrapType.Reverse) TempEnemyController.ToggleFrenzyStateWithTimer(duration);
            if (type == TrapType.Freeze) TempEnemyController.SetTemporaryMovementSpeed(TempEnemyController.ChaseSpeed * SpeedModifier, duration);
            if (type == TrapType.Slow) TempEnemyController.SetTemporaryMovementSpeed(TempEnemyController.ChaseSpeed * SpeedModifier, duration);

        }


        if (AreaEffect == true && CurrentTime <= 0f && ( other.tag == "Player" || other.tag == "Enemy"))
        {
            Debug.Log("trap Activate");
            UsageLeft--;
            CurrentTime += 1;
            Collider[] colliders = Physics.OverlapSphere(transform.position, AreaEffectRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if( colliders[i].gameObject.tag == "Player")
                {
                    if(colliders[i].GetComponent<PlayerController>() != null)
                    {
                        target = colliders[i].GetComponent<PlayerController>();
                        if (type == TrapType.Freeze) FreezeTrap();
                        if (type == TrapType.Reverse) ReverseTrap(aflictionValue);
                        if (type == TrapType.Slow) SlowTrap(aflictionValue);
                        if (type == TrapType.Damage) colliders[i].GetComponent<HealthComp>().TakeDamage(TrapDamage);
                    }
                }

                if(colliders[i].gameObject.tag == "Enemy")
                {
                    if(colliders[i].gameObject.GetComponent<Controller>() != null)
                    {
                        Controller EnemyController = colliders[i].gameObject.GetComponent<Controller>();
                        if (type == TrapType.Reverse) EnemyController.ToggleFrenzyStateWithTimer(duration);
                        if (type == TrapType.Freeze) EnemyController.SetTemporaryMovementSpeed(EnemyController.ChaseSpeed * SpeedModifier, duration);
                        if (type == TrapType.Slow) EnemyController.SetTemporaryMovementSpeed(EnemyController.ChaseSpeed * SpeedModifier, duration);
                    }
                    if (type == TrapType.Damage) colliders[i].GetComponent<HealthComp>().TakeDamage(TrapDamage);
                }
            }
        }

        if(UsageLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SlowTrap(float slow)
    {
        target.HitByTrap(reverse, slow, duration);
    }

    private void ReverseTrap(float reverse)
    {
        target.HitByTrap(reverse, slow, duration);
    }

    private void FreezeTrap()
    {
        target.SetFreeze(duration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
        if (AreaEffect)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, AreaEffectRadius);
        }

    }
}
