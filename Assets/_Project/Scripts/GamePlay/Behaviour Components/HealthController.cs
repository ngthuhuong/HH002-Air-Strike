using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100; // Maximum health
    [SerializeField] private int currentHealth; // Current health
    public bool IsDead => currentHealth <= 0;
    
    [SerializeField] private UnityEvent onDead;

    #region MonoBehaviour

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    #endregion

    #region Public Methods

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // health bar 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to heal
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    #endregion

    #region Private Methods

    // Method to handle death
    private void Die()
    {
        onDead?.Invoke();
        
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject); // Destroy the object (optional)
    }

    #endregion
}