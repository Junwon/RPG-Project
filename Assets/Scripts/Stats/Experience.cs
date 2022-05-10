using UnityEngine;
using System;

using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expPoints = 0.0f;

        public event Action onExperiencedGained;

        public void GainExp(float exp)
        {
            expPoints += exp;
            onExperiencedGained();
        }

        public float GetPoints()
        {
            return expPoints;
        }

        public object CaptureState()
        {
            return expPoints;
        }

        public void RestoreState(object state)
        {
            expPoints = (float)state;
        }
    }
}