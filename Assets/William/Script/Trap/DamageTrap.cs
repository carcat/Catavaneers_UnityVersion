using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] float Damage;
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.GetComponent<HealthComp>() != null)
        {
<<<<<<< HEAD
            //other.gameObject.GetComponent<HealthComp>().TakeDamage(Damage);
            Destroy(gameObject);
=======
            if (other.gameObject.GetComponent<HealthComp>().myClass == CharacterClass.Player)
            {
                other.gameObject.GetComponent<HealthComp>().TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<HealthComp>().myClass == CharacterClass.Enemy)
            {

            }
>>>>>>> 8519fa8df6a8b00e22feff8c8aa1d4005c7cf81b
        }
    }
}
