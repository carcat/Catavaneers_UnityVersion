using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTrap : MonoBehaviour
{
    [SerializeField] float BactToNormal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().HidtByReverseTrap(true, BactToNormal);
            Debug.Log("ReverseControl");
            Destroy(gameObject);
        }
    }
}
