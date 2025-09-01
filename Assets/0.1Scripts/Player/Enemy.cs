using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

     private float maxHealth = 10;
     [SerializeField] private float currHealth;
    private void Awake()
    {
        healthBar = GetComponent<HealthBar>();
    }

    void Start()
    {
        currHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth,currHealth);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand_R"))
        {
            
        }
    }

    public void TakeDamage(float amount)
    {
        currHealth -= amount;
        healthBar.UpdateHealthBar(maxHealth, currHealth);
        Debug.Log(currHealth);
    }
}
