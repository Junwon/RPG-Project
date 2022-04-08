using UnityEngine;

namespace RPG.Core
{
    public class DestroyAffectEffect : MonoBehaviour
    {
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
