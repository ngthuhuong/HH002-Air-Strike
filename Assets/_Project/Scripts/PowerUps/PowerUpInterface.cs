using UnityEngine;

public interface PowerUpInterface
{
    void ApplyPowerUp(GameObject target);
    void RemovePowerUp(GameObject target);
    float GetDuration();
    string GetName();
} 