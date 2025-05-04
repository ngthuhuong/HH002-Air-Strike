using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private int attackDamage = 10; 
    public string targetTag = TagConst.Player; // Tag of the target to attack
    public int AttackDamage
    {
        get => attackDamage;
        set => attackDamage = value;
    }
    
    [SerializeField] private float attackCooldown = 1f; // Time between attacks
    private float currentCooldown = 0f; // Tracks cooldown time

    #region MonoBehaviour

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(targetTag))
        {
            HealthController targetHealth = other.GetComponent<HealthController>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
            }
        }
    }

    #endregion

    #region Public Methods

    // Method to perform an attack
    public void Attack(HealthController target)
    {
        if (target != null)
        {
            target.TakeDamage(attackDamage);
            Debug.Log($"{gameObject.name} attacked {target.name} for {attackDamage} damage.");
        }
        
        /*if (currentCooldown <= 0)
        {
            if (target != null)
            {
                target.TakeDamage(attackDamage);
                Debug.Log($"{gameObject.name} attacked {target.name} for {attackDamage} damage.");
            }

            currentCooldown = attackCooldown; // Reset cooldown
        }
        else
        {
            Debug.Log("Attack is on cooldown.");
        }*/
    }

    #endregion
}