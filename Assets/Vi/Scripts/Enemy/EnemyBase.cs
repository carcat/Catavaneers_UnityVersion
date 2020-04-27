using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum Type
    {
        Mouse,
        Cat,
        Dog
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
        protected List<Transform> targets;
        protected HealthComp healthComp;
        protected Rigidbody rb;

        public virtual void Start()
        {
            healthComp = GetComponent<HealthComp>();
            rb = GetComponent<Rigidbody>();
        }

        public virtual void KnockedBack(float knockBackForce)
        {

        }

        private void OnDrawGizmosSelected()
        {
            if (specification.visualRange > 0)
                Gizmos.DrawWireSphere(transform.position, specification.visualRange);
        }
    }
}