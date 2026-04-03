using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    private float maxHealth = 1f;

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        healthBarImage.fillAmount = 1f;
    }

    public void SetHealth(float currentHealth)
    {
        float fill = currentHealth / maxHealth;
        healthBarImage.fillAmount = fill;
    }
}