using UnityEngine;

using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expPoints = 0.0f;

        public void GainExp(float exp)
        {
            expPoints += exp;
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