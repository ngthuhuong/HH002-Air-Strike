using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100; // Maximum health
    [SerializeField] private float currentHealth; 
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsDead => currentHealth <= 0;
    
    [SerializeField] private UnityEvent onDead;
    [SerializeField] private UnityEvent onTakeDamage;
    

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
        onTakeDamage?.Invoke();

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