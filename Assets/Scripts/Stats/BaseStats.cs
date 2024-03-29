﻿using System;
using UnityEngine;

using RPG.Utils;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass = CharacterClass.Player;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action onLevelUp;

        LazyVar<int> currentLevel;
        Experience experience;

        void Awake()
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyVar<int>(CalculateLevel);
        }

        void Start()
        {
            currentLevel.Init();
        }

        void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperiencedGained += UpdateLevel;
            }
        }

        void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperiencedGained -= UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value) {
                currentLevel.value = newLevel;
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
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentModifier(stat) * 0.01f);
        }

        public int GetLevel()
        {
            return currentLevel.value;
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

        float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0.0f;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        float GetPercentModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0.0f;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
    }
}
