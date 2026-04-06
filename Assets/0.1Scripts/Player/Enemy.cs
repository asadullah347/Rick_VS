using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float knockbackMultiplier = 1f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthBar == null)
            healthBar = GetComponentInChildren<HealthBar>();

        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        healthBar?.SetMaxHealth(maxHealth);
        healthBar?.SetHealth(currentHealth);
    }

    public void TakeDamage(float amount, Vector3 hitDirection, float knockbackForce)
    {
        currentHealth -= amount;
        healthBar?.SetHealth(currentHealth);

        if (rb != null)
        {
            Vector3 force = (hitDirection + Vector3.up * 0.2f).normalized * knockbackForce * knockbackMultiplier;
            rb.AddForce(force, ForceMode.Impulse);
        }
        else
        {
            transform.position += hitDirection * 0.15f * knockbackMultiplier;
        }

        if (currentHealth <= 0f)
            Destroy(gameObject);
    }
}