using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using MoreMountains.Tools;

public class HealthController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    public float CurrentHealth { get => currentHealth;
        set
        {
            currentHealth = value;
            MMEventManager.TriggerEvent(new EDataChanged());
        }
    }
    public bool IsDead => currentHealth <= 0;

    [Header("Invincibility Settings")]
    private float invincibleDuration = 0.2f;
    private bool isInvincible = false; 
    private IEnumerator invincibleCoroutine;
    [SerializeField] private GameObject shieldVFX; 

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
            // StartCoroutine(IEActivateInvincibility(invincibleDuration));
        }
    }

    // Method to heal
    public void Heal(int amount)
    {
        MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.SFX_Heal));
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
    }

    public void ActivateShield(float duration)
    {
        if (invincibleCoroutine != null)
        {
            DeactivateInvincibility();
        }

        invincibleCoroutine = IEActiveShield(duration);
        StartCoroutine(invincibleCoroutine);
    }

    public void DeactivateInvincibility()
    {
        StopCoroutine(invincibleCoroutine);
        isInvincible = false;
        
        if (shieldVFX != null)
            shieldVFX.SetActive(false);
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
    private IEnumerator IEActivateInvincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    private IEnumerator IEActiveShield(float duration)
    {

        MMEventManager.TriggerEvent(new EActiveBooster(BoosterType.Shield, duration));
        isInvincible = true;
        if (shieldVFX != null)
        {
            shieldVFX.SetActive(true);
            MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.SFX_Active_Shield));
        }
        
        yield return new WaitForSeconds(duration);
        isInvincible = false;
        if (shieldVFX != null)
            shieldVFX.SetActive(false);
    }

    #endregion
}