using System;
using System.Collections;
using UnityEngine;

public class PowerUpSpeedBoost : PowerUpBase
{
    private float originalSpeed;
    private MoveController moveController;
    //[SerializeField] IEnumerator currentCoroutine;
    
    [Header("Flags")]
    [SerializeField] private bool isActivated = false;
    

    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated) return;
        if (other.CompareTag(TagConst.Player))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            ApplyPowerToPlayer(player);
            isActivated = true;
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region Private Methods

    private void ApplyPowerToPlayer(PlayerController player)
    {
        if (player == null) return;
        // player.SpeedMultiplier = powerUpData.effectValue;
        player.ApplySpeedBoost(powerUpData.duration, powerUpData.effectValue);
    }

    #endregion

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