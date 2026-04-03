using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthBar == null)
            healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        healthBar?.SetMaxHealth(maxHealth);
        healthBar?.SetHealth(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy HP: " + currentHealth);
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}