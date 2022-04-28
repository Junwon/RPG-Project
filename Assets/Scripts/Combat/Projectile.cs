using UnityEngine;

using RPG.Attributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1.0f;
        [SerializeField] bool isHoming = true;
        [SerializeField] float maxLifeTime = 10.0f;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2.0f;

        Health target = null;
        GameObject instigator = null;
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

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
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
                target.TakeDamage(instigator, damage);

                speed = 0.0f;

                if (hitEffect != null)
                {
                    Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                }
                
                foreach (GameObject toDestroy in destroyOnHit)
                {
                    Destroy(toDestroy);
                }

                Destroy(gameObject, lifeAfterImpact);
            }
        }
    }
}
