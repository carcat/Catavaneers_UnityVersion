using UnityEngine;

public class Trap : MonoBehaviour
{    public enum TrapType
    {
        Freeze,
        Reverse,
        Slow
    }

    [SerializeField] TrapType type;
    [SerializeField] float aflictionValue = 0.0f;
    [SerializeField] float duration = 1;
    PlayerController target;
    int reverse = 1;
    float slow = 1;

    //below is edit by Will
    float ActivateTimer = 3;
    float CurrentTime;

    private void Start()
    {
        CurrentTime = ActivateTimer;
    }

    private void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" & CurrentTime <= 0f)
        {
            Debug.Log("Player in trap = " + type);
            target = other.GetComponent<PlayerController>();
            if (type == TrapType.Freeze) FreezeTrap();
            if (type == TrapType.Reverse) ReverseTrap(aflictionValue);
            if (type == TrapType.Slow) SlowTrap(aflictionValue);
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
    }
}
