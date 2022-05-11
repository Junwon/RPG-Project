using UnityEngine;

using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;
        
        bool isDead = false;

        void Start()
        {
            if (healthPoints <= 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            healthPoints = Mathf.Max(healthPoints - damage, 0f);

            if (healthPoints <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        void AwardExperience(GameObject instigator)
        {
            Experience exp = instigator.GetComponent<Experience>();
            if (exp == null) return;

            exp.GainExp(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        
        public float GetPercentage()
        {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health) * 100.0f;
        }

        void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void RegenerateHealth()
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
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
