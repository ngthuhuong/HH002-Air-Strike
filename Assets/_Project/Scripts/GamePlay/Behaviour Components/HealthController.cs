using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsDead => currentHealth <= 0;

    [Header("Invincibility Settings")]
    private float invincibleDuration = 0.2f;
    private bool isInvincible = false; 

    [SerializeField] private UnityEvent onDead;
    [SerializeField] private UnityEvent onTakeDamage;

    #region MonoBehaviour

    private void Start()
    {
        currentHealth = maxHealth;
    }

    #endregion

    #region Public Methods

    // Method to take damage
    public void TakeDamage(int damage)
    {
        if (isInvincible || IsDead) return; // Ignore damage if invincible or dead

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp health
        onTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ActivateInvincibility());
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
        //Destroy(gameObject);
    }

    // Coroutine to activate invincibility
    private System.Collections.IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    #endregion
}