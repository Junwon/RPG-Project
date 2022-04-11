using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text healthText = null;

        void Awake()
        {
            healthText = GetComponent<Text>();
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        void Update()
        {
            healthText.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}
