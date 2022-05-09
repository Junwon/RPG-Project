using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        Text baseStatText = null;
    
        void Awake()
        {
            baseStatText = GetComponent<Text>();
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }
    
        void Update()
        {
            baseStatText.text = String.Format("{0:0}", baseStats.GetLevel());
        }
    }
}
