using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass = CharacterClass.Player;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        public event Action onLevelUp;

        int currentLevel = 0;

        void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null) {
                experience.onExperiencedGained += UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel) {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel()) + GetAdditiveModifier(stat);
        }

        public int GetLevel()
        {
            if (currentLevel <= 0)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        int CalculateLevel()
        {
            Experience exp = GetComponent<Experience>();

            if (exp == null) return startingLevel;

            float currrentXP = exp.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level < penultimateLevel; ++level)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (xpToLevelUp > currrentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
    }
}
