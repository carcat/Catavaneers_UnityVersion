using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrap : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<PlayerController>().HitByfreezeTrap(true,3f);

            

    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().HitByfreezeTrap(true, 3f);
            Debug.Log("Freeze");
            Destroy(gameObject);
        }
    }
}
