using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUps/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    public string powerUpName;
    public Sprite icon;
    public float duration = 10f;
    public float effectValue = 1f;
    public GameObject powerUpPrefab;
    public AudioClip activationSound;
    public ParticleSystem activationEffect;
} 