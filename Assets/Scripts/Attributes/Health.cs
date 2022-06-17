using System;
using UnityEngine;
using UnityEngine.Events;

using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using RPG.Utils;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] UnityEvent<float> takeDamage;
        [SerializeField] HealthBar healthBar = null;
        [SerializeField] UnityEvent onDie;

        LazyVar<float> healthPoints;
        
        bool isDead = false;

        void Awake()
        {
            healthPoints = new LazyVar<float>(GetInitialHealth);
        }

        void Start()
        {
            healthPoints.Init();
            healthBar?.Init(this);
        }

        float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0f);
            takeDamage.Invoke(damage);

            healthBar?.UpdateHealthBar();

            if (healthPoints.value <= 0)
            {
                onDie.Invoke();
                healthBar?.SetVisible(false);
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
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        
        public float GetPercentage()
        {
            return GetFraction() * 100.0f;
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
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
            healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            float health = (float)state;
            healthPoints.value = health;
            if (healthPoints.value <= 0)
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
