using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] float Damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<HealthComp>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
