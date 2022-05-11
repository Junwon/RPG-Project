using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Health enemy = null;
        Fighter player = null;
        Text healthText = null;

        void Awake()
        {
            healthText = GetComponent<Text>();
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if (enemy = player.GetTarget())
            {
                healthText.text = String.Format("{0:0}/{1:0}", enemy.GetHealthPoints(), enemy.GetMaxHealthPoints());
            }
            else
            {
                healthText.text = "N/A";
            }
        }
    }
}
