using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum Type
    {
        Mouse,
        Cat,
        Dog,
        None
    }

    [System.Serializable]
    public struct Specification
    {
        public float visualRange;
        public float moveSpeed;
    }

    [RequireComponent(typeof(HealthComp), typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected Specification specification;
        [SerializeField] protected float weaponDamage; // TODO change to actual weapon damage
        [SerializeField] protected Transform[] patrolPoints;

        protected Type type = Type.None;
        protected Transform target;
        protected HealthComp healthComp;
        protected Rigidbody rb;

        public Specification Specs { get { return specification; } }
        public Transform[] PatrolPoints { get { return patrolPoints; } }

        virtual protected void Start()
        {
            healthComp = GetComponent<HealthComp>();
            rb = GetComponent<Rigidbody>();
        }

        virtual protected void KnockedBack(float knockBackForce)
        {

        }

        private void OnDrawGizmosSelected()
        {
            if (specification.visualRange > 0)
                Gizmos.DrawWireSphere(transform.position, specification.visualRange);
        }
    }
}