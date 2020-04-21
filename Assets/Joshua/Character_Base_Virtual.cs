using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Base_Virtual : MonoBehaviour
{
    [SerializeField]
    protected float health_float = 100;
    [SerializeField]
    protected float maximum_health_float = 100;
    [SerializeField]
    protected bool is_dead = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        Purpose: Deal damage to character
        Effects: removes damage ammount from health
        Input/Output: damage_float
        Global Variables Used: health_float
    */
    public void TakeDamage(float damage_float)
    {
        health_float -= damage_float;
        if (health_float <= 0)
        {
            health_float = 0;
            is_dead = true;
        }
    }

    /*
        Purpose: returns character's health
        Effects: returns health_float
        Input/Output: health_float
        Global Variables Used: health_float
    */
    public float GetHealth()
    {
        return health_float;
    }

    /*
        Purpose: returns wether character is dead
        Effects: returns is_dead bool
        Input/Output: is_dead
        Global Variables Used: is_dead
    */
    public bool IsDead()
    {
        return is_dead;
    }

    /*
        Purpose: heals character up to maximum health
        Effects: increases health_float by heal_float amount
        Input/Output: heal_float
        Global Variables Used: health_float, maximum_health_float
    */
    public void HealHealth(float heal_float)
    {
        health_float += heal_float;
        if (health_float > maximum_health_float)
        {
            health_float = maximum_health_float;
        }
    }




}
