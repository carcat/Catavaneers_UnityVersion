﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string rightThumbstickX;
    [SerializeField] string rightThumbstickY;
    [SerializeField] string leftThumbstickx;
    [SerializeField] string leftThumbstickY;
    [SerializeField] float speed = 0.0f;
    [SerializeField] float dodgeSpeed = 0.0f;
    [SerializeField] float straffSensitiviy = 30.0f;

    Vector3 LTumbInput = new Vector3(0,0,0);
    Vector3 RTumbInput = new Vector3(0, 0, 0);
    float leftInputMagnitud = 0.0f;
    float characterRotation = 0.0f;
    Character_Base_Virtual characterBase;

    [SerializeField] bool IsFreeze = false;

    private void Start()
    {
        characterBase = GetComponent<Character_Base_Virtual>();
    }

    void Update()
    {
        if (!characterBase.IsDead())
        {
            AxisInput();
            CharacterMove();
            Rotation();
            Direction();
            if (Input.GetButtonDown("Dodge"))
            {
                GetComponent<Animator>().SetTrigger("Roll");
            }
        }

    }

    private void CharacterMove()
    {
        if(IsFreeze != true)
        {
            GetComponent<Animator>().SetFloat("Walk", leftInputMagnitud);
            transform.position += LTumbInput * speed * Time.deltaTime * leftInputMagnitud;
        }
    }

    private void Rotation()
    {
        if (RTumbInput != Vector3.zero)
        {
            characterRotation = Mathf.Atan2(Input.GetAxis("Horizontal Right Thumbstick"), Input.GetAxis("Vertical Right Thumbstick")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, characterRotation, 0));
        }
    }

    private void AxisInput()
    {
        LTumbInput = new Vector3(Input.GetAxis("Horizontal Left Thumbstick"), 0, Input.GetAxis("Vertical Left Thumbstick"));
        RTumbInput = new Vector3(Input.GetAxis("Horizontal Right Thumbstick"), 0, Input.GetAxis("Vertical Right Thumbstick"));
        leftInputMagnitud = LTumbInput.magnitude;
    }

    //function that tells the animator if players is strafing and the direction
    private void Direction()
    {
        float curAngle = Vector3.Angle(LTumbInput.normalized, transform.forward);

        float clockwise = angleDir(transform.forward, LTumbInput.normalized, Vector3.up);
        if (curAngle < straffSensitiviy)
        {
            GetComponent<Animator>().SetInteger("Strafe", 0);
        }
        if ((curAngle > straffSensitiviy && curAngle < 180 - straffSensitiviy && clockwise < 0))
        {
            GetComponent<Animator>().SetInteger("Strafe", -1);
        }
        if ((curAngle > straffSensitiviy && curAngle < 180- straffSensitiviy && clockwise > 0))
        {
            GetComponent<Animator>().SetInteger("Strafe", 1);
        }
        if ((curAngle > 180 - straffSensitiviy))
        {
            GetComponent<Animator>().SetInteger("Strafe", 0);
        }
    }

    //complements the Direction function, tells if the angle of straffing in clockwise or counter clockwise
    public float angleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {

        Vector3 perp = Vector3.Cross(fwd, targetDir);

        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0)
        {
            return 1.0f;
        }
        else if (dir < 0.0)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    public void HitByfreezeTrap(bool HitTrap, float time)
    {
        IsFreeze = HitTrap;
        StartCoroutine(UnFreeze(time));
    }

    private IEnumerator UnFreeze(float Freezetime)
    {
        yield return new WaitForSeconds(Freezetime);
        IsFreeze = false;
    }
}
