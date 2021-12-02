using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Magicko.UI
{
    
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public TextMeshProUGUI textBox;
        public Image fill;

        private void Awake()
        {
            textBox = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        }

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;

            textBox.text = health.ToString();
            fill.color = gradient.Evaluate(1f);
        }

        public void SetHealth(float hp)
        {
            slider.value = hp;
            textBox.text = hp.ToString();

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}