using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider healthSlider = null;
        
        Health healthComponent = null;

        public void Init(Health health)
        {
            healthComponent = health;
            UpdateHealthBar();
            SetVisible(false);
        }

        public void UpdateHealthBar()
        {
            SetVisible(true);
            healthSlider.value = healthComponent.GetFraction();
        }

        public void SetVisible(bool visible = true)
        {
            Canvas canvas = healthSlider.GetComponentInParent<Canvas>();
            canvas.enabled = visible;
        }
    }
}
