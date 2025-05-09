using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour, PowerUpInterface
{
    [SerializeField] protected PowerUpData powerUpData;
    protected bool isActive = false;
    protected float remainingDuration = 0f;

    public virtual void ApplyPowerUp(GameObject target)
    {
        if (isActive) return;
        
        isActive = true;
        remainingDuration = powerUpData.duration;
        
        if (powerUpData.activationSound != null)
        {
            AudioSource.PlayClipAtPoint(powerUpData.activationSound, target.transform.position);
        }
        
        if (powerUpData.activationEffect != null)
        {
            Instantiate(powerUpData.activationEffect, target.transform.position, Quaternion.identity);
        }
    }

    public virtual void RemovePowerUp(GameObject target)
    {
        isActive = false;
        remainingDuration = 0f;
    }

    public float GetDuration()
    {
        return powerUpData.duration;
    }

    public string GetName()
    {
        return powerUpData.powerUpName;
    }

    protected virtual void Update()
    {
        if (isActive)
        {
            remainingDuration -= Time.deltaTime;
            if (remainingDuration <= 0)
            {
                RemovePowerUp(gameObject);
            }
        }
    }
} 