using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1.0f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;

        Health target = null;
        float damage = 0f;

        void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height * 0.5f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() == target && !target.IsDead())
            {
                target.TakeDamage(damage);
                if (hitEffect)
                {
                    Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
