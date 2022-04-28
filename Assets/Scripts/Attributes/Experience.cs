using RPG.Saving;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expPoints = 0.0f;

        public void GainExp(float exp)
        {
            expPoints += exp;
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