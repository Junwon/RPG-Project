using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience exp;
        Text expText = null;
    
        void Awake()
        {
            expText = GetComponent<Text>();
            exp = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
    
        void Update()
        {
            expText.text = String.Format("{0:0}", exp.GetPoints());
        }
    }
}
