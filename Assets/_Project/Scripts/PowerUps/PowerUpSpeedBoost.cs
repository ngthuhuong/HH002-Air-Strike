using UnityEngine;

public class PowerUpSpeedBoost : PowerUpBase
{
    private float originalSpeed;
    private MoveController moveController;

    public override void ApplyPowerUp(GameObject target)
    {
        base.ApplyPowerUp(target);
        
        moveController = target.GetComponent<MoveController>();
        if (moveController != null)
        {
            moveController.ApplySpeedBoost(powerUpData.duration, powerUpData.effectValue);
        }
    }

    public override void RemovePowerUp(GameObject target)
    {
        if (moveController != null)
        {
            moveController.ApplySpeedBoost(0f, 0f);
        }
        
        base.RemovePowerUp(target);
    }
} 