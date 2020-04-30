using System.Collections;
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
    HealthComp health;
    float weaponWeight = 1;
    float  inverted = 1;

    bool freeze = false;
    float reverseValue = 1;
    float slowValue = 1;


    //Add by Will
    [SerializeField] bool IsFreeze = false;
    [SerializeField] bool IsReverse = false;
    [SerializeField] bool IsSlow = false;
    [SerializeField] float SlowedSpeed; 

    private void Start()
    {
        health = GetComponent<HealthComp>();
    }

    void Update()
    {
        if (!health.IsDead())
        {
            Movement();
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Die");
        }

    }

    private void Movement()
    {
        AxisInput();
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

    public void SetWeaponWeight(float currentWeapon)
    {
        weaponWeight = currentWeapon;
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
        if(freeze == true)
        {
            LTumbInput = Vector3.zero;
            RTumbInput = Vector3.zero;
        }
        else
        {
            LTumbInput = new Vector3(Input.GetAxis("Horizontal Left Thumbstick"), 0, Input.GetAxis("Vertical Left Thumbstick"));
            RTumbInput = new Vector3(Input.GetAxis("Horizontal Right Thumbstick"), 0, Input.GetAxis("Vertical Right Thumbstick"));
        }
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

    public void HitByTrap(bool freezeTrap, float reverseTrapValue, float slowTrapValue, float lastingTime)
    {
        freeze = freezeTrap;
        reverseValue = reverseTrapValue;
        slowValue = slowTrapValue;
        StartCoroutine(UndoAfliction(lastingTime));
    }

    private IEnumerator UndoAfliction(float time)
    {
        yield return new WaitForSeconds(time);
        freeze = false;
        reverseValue = 1;
        slowValue = 1;
    }

    public bool GetFreeze()
    {
        return freeze;
    }
}
