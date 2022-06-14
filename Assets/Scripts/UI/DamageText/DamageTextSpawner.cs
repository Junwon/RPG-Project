using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefabs = null;

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefabs, transform);
            instance.SetValue(damageAmount);
        }
    }
}
