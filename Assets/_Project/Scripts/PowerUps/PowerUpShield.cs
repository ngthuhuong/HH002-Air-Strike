using System;
using UnityEngine;

public class PowerUpShield : PowerUpBase
{
    private GameObject shieldObject;
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
            healthController.ActivateShield(powerUpData.duration);
        }
    }

    /*public override void RemovePowerUp(GameObject target)
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
    }*/
} 