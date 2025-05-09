using UnityEngine;

public class PowerUpShield : PowerUpBase
{
    private GameObject shieldObject;
    private HealthController healthController;

    public override void ApplyPowerUp(GameObject target)
    {
        base.ApplyPowerUp(target);
        
        healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {
            // Create shield visual effect
            if (powerUpData.powerUpPrefab != null)
            {
                shieldObject = Instantiate(powerUpData.powerUpPrefab, target.transform);
            }
            
            // Enable invincibility
            healthController.ActivateInvincibility(powerUpData.duration);
        }
    }

    public override void RemovePowerUp(GameObject target)
    {
        if (healthController != null)
        {
            healthController.DeactivateInvincibility();
        }
        
        if (shieldObject != null)
        {
            Destroy(shieldObject);
        }
        
        base.RemovePowerUp(target);
    }
} 