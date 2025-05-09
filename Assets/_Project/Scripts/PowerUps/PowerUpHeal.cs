using UnityEngine;

public class PowerUpHeal : PowerUpBase
{
    private HealthController healthController;

    public override void ApplyPowerUp(GameObject target)
    {
        base.ApplyPowerUp(target);
        
        healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.Heal((int)powerUpData.effectValue);
        }
    }
} 