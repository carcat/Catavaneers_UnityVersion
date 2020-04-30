﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum CharacterStates
    {
        Movement,
        Freeze,
        Dodge
    }
    [SerializeField] float speed = 0.0f;
    [SerializeField] float dodgeSpeed = 0.0f;
    [SerializeField] float straffSensitiviy = 30.0f;

    Vector3 LTumbInput = new Vector3(0,0,0);
    Vector3 RTumbInput = new Vector3(0, 0, 0);
    float leftInputMagnitud = 0.0f;
    float characterRotation = 0.0f;
    CharacterStates states = CharacterStates.Movement;
    HealthComp health;
    float weaponWeight = 1;

    bool freeze = false;
    float reverseValue = 1;
    float slowValue = 1;

    private void Start()
    {
        health = GetComponent<HealthComp>();
    }

    void Update()
    {
        if (!health.IsDead())
        {
            switch(states)
            {
                case CharacterStates.Dodge:
                    break;
                case CharacterStates.Freeze:
                    leftInputMagnitud = 0;
                    break;
                default:
                    AxisInput();
                    break;
            }
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Die");
        }

    }
    private void AxisInput()
    {
            LTumbInput = new Vector3(Input.GetAxis("Horizontal Left Thumbstick"), 0, Input.GetAxis("Vertical Left Thumbstick"));
            RTumbInput = new Vector3(Input.GetAxis("Horizontal Right Thumbstick"), 0, Input.GetAxis("Vertical Right Thumbstick"));
            leftInputMagnitud = LTumbInput.magnitude;
        CharacterMove(weaponWeight, reverseValue, slowValue);
        Rotation();
        Direction();
    }

    private void CharacterMove(float weight, float reverse, float slow)
    {
        float movementFraction = weight * speed * reverse* leftInputMagnitud;
        movementFraction = movementFraction / slow;
        GetComponent<Animator>().SetFloat("Walk", leftInputMagnitud);
        transform.position += LTumbInput * Time.deltaTime * movementFraction;
    }
    private void Rotation()
    {
        if (RTumbInput != Vector3.zero)
        {
            characterRotation = Mathf.Atan2(Input.GetAxis("Horizontal Right Thumbstick"), Input.GetAxis("Vertical Right Thumbstick")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, characterRotation, 0));
        }
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

    public void SetWeaponWeight(float currentWeapon)
    {
        weaponWeight = currentWeapon;
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

    public void HitByTrap(float reverseTrapValue, float slowTrapValue, float lastingTime)
    {
        reverseValue = reverseTrapValue;
        slowValue = slowTrapValue;
        StartCoroutine(UndoAfliction(lastingTime));
    }

    private IEnumerator UndoAfliction(float time)
    {
        yield return new WaitForSeconds(time);
        states = CharacterStates.Movement;
        reverseValue = 1;
        slowValue = 1;
    }

    public CharacterStates GetCharacterState()
    {
        return states;
    }

    public void SetFreeze(float lastingTime)
    {
        states = CharacterStates.Freeze;
        StartCoroutine(UndoAfliction(lastingTime));
    }
}
