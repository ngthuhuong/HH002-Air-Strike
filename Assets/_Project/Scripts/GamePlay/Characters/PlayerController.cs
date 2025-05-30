using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AttackController attackController;
    public HealthController healthController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider2D;

    [Header("Colors")]
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color normalColor;
    
    [Header("Flags")]
    [SerializeField] private bool isSpeedBoosted = false;
    
    public float CurrentHealth
    {
        get => healthController.CurrentHealth;
    }
    
    [Header("Stats")]
    public float baseSpeed = 5f; // Speed of the player movement
    [SerializeField] private float speedMultiplier = 1f;
    public float SpeedMultiplier
    {
        get => speedMultiplier;
        set => speedMultiplier = value;
    }
    public float CurrentSpeed
    {
        get => baseSpeed * speedMultiplier;
    }
    
    
    public float timeInterval = 0.2f; // Time interval between shots
    [SerializeField] private float stoppingDistance = 0.1f;

    [Header("Shooting")]
    public GameObject projectilePrefab; // Prefab for the bullet
    public Transform projectileSpawnPoint; // Spawn point for the bullet
    public Transform projectileContainer; // Parent container for projectiles
    private float currentTimer = 0f; // Tracks the countdown timer

    [Header("Movement")]
    private Vector3 targetPosition;

    [Header("Flags")]
    [SerializeField, MMReadOnly] private bool isMoving = false;
    [SerializeField] private bool isMovable = true; 
    

    [Header("Behaviour")] 
    [SerializeField] private ExplosionController dieVFX;
    
    #region MonoBehaviour

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        HandleMovement();
        ShootBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Coin))
        {
            Debug.Log(other.name);
            MMEventManager.TriggerEvent(new EEarnResource(ResourceType.Coin, 1));
            MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.SFX_Collect_Coin));
        }
    }

    #endregion

    #region Editor Methods

    [ContextMenu("Reset Values")]
    private void ResetValues()
    {
        baseSpeed = 5f;
        timeInterval = 0.1f;
        stoppingDistance = 0.1f;
        isMoving = false;
        currentTimer = 0f;
        targetPosition = Vector3.zero;

        Debug.Log("Player values have been reset.");
    }

    #endregion
    #region Private Methods

    private void HandleMovement()
    {
        if (!isMovable) return;
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, CurrentSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance)
            {
                isMoving = false;
            }
        }
    }

    private void ShootBullet()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            // Decrease the timer
            currentTimer -= Time.deltaTime;

            // Check if the timer has reached zero
            if (currentTimer <= 0f)
            {
                // GameObject projectile = PoolingManager.Instance.GetObject(PoolingManager.PoolTag.PlayerProjectile, projectileSpawnPoint.position, Quaternion.identity);
                
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                if (projectileContainer != null)
                    projectile.transform.SetParent(projectileContainer);
                
                // Set attributes for the projectile
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                
                projectileController.attackController.AttackDamage = attackController.AttackDamage; 
                projectileController.moveController.Direction = Vector2.up;
                
                MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.SFX_Shoot));
                
                currentTimer = timeInterval; // Reset the timer
            }
        }
    }

    private IEnumerator IDie()
    {
        // Disable the player
        collider2D.enabled = false;
        spriteRenderer.enabled = false;
        isMovable = false;

        // Play the die VFX
        dieVFX.gameObject.SetActive(true);

        yield return new WaitForSeconds(dieVFX.Animator.GetCurrentAnimatorStateInfo(0).length); // Wait for 1 second

        // Trigger game over event
        MMEventManager.TriggerEvent(new EGameOver());
    }

    private IEnumerator IHurtEffect()
    {
        spriteRenderer.color = hurtColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = normalColor;
    }
    #endregion

    #region Public Methods

    public void OnPlayerDie()
    {
        StartCoroutine(IDie());
    }

    public void OnPlayerTakeDamage()
    {
        MMEventManager.TriggerEvent(new EDataChanged());
        StartCoroutine(IHurtEffect());
    }

    public void ApplySpeedBoost(float duration, float speedMultiplier)
    {
        MMEventManager.TriggerEvent(new EActiveBooster(BoosterType.Speed, duration));
        StartCoroutine(IEApplySpeedBoost(duration, speedMultiplier));
        
    }

    #endregion

    #region Power Ups Methods
    
    public IEnumerator IEApplySpeedBoost(float duration, float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
        yield return new WaitForSeconds(duration);
        this.speedMultiplier = 1f;
    }

    #endregion
}