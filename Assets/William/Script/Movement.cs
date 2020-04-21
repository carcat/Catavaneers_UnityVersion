using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//WIlliam's Script

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    Vector2 input;
    Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1f;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float moveHorizontal = Input.GetAxisRaw("Horizontal");
        //float moveVertical = Input.GetAxisRaw("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //transform.rotation = Quaternion.LookRotation(movement);

        //transform.Translate(movement * moveSpeed * Time.deltaTime);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        input = new Vector2();
        input.x = horizontal;
        input.y = vertical;
    }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.right * input.x * moveSpeed * Time.deltaTime;
        movement += Vector3.forward * input.y * moveSpeed * Time.deltaTime;
        movement.y = 0.0f;
        Vector3 targetPosition = rigidBody.position + movement;

    }
}
