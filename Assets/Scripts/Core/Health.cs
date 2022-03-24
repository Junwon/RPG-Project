using UnityEngine;

using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);

            if (healthPoints <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            float health = (float)state;
            healthPoints = health;

            if (healthPoints <= 0)
            {
                Die();
            }
            else
            {
                if (isDead == true)
                {
                    Animator anim = gameObject.GetComponentInChildren<Animator>();

                    anim.ResetTrigger("die");
                    anim.ResetTrigger("stopAttack");

                    anim.Rebind();
                    anim.Update(0f);

                    isDead = false;
                }
            }
        }
    }
}
