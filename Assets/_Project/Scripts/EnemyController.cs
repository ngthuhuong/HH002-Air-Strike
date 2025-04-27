using System;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private AttackController attackController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private MoveController moveController;
    
    
    #region MonoBehaviour

    private void Start()
    {
        
    }

    void Update()
    {
        CheckIfOutsideScreen();
    }

    private void OnDestroy()
    {
        MMEventManager.TriggerEvent(new EEnemyDie());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Player))
        {
            HealthController player = other.GetComponent<HealthController>();
            if (player != null)
            {
                Debug.Log($"Player takes damage: {attackController.AttackDamage}");
                player.TakeDamage(attackController.AttackDamage);
                Destroy(gameObject); // Destroy the enemy
            }
        }
    }

    #endregion

    #region Private Methods

    private void CheckIfOutsideScreen()
    {
        // Check if the projectile is outside the screen
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y < ConstValue.ViewportMinY)
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }

    #endregion
    
    #region

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}