using System;
using UnityEngine;

public class PowerUpHeal : PowerUpBase
{
    private HealthController healthController;

    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthController>() != null)
        {
            ApplyPowerUp(other.gameObject);
            gameObject.SetActive(false);
        }
        
    }

    #endregion

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