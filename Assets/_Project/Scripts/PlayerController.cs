using System;
using MoreMountains.Tools;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AttackController attackController;
    [SerializeField] private HealthController healthController;
    
    [Header("Stats")]
    public float speed = 5f; // Speed of the player movement
    public float timeInterval = 0.05f; // Time interval between shots
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

    #region MonoBehaviour

    void Update()
    {
        HandleMovement();
        ShootBullet();
    }

    
    #endregion

    #region Editor Methods

    [ContextMenu("Reset Values")]
    private void ResetValues()
    {
        speed = 5f;
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
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

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
                float diagonalAngle = 45f;
                Quaternion rotation = Quaternion.Euler(0, 0, diagonalAngle);
                Vector2 rotation2 = new Vector2(1f, 1f);
                
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                projectile.GetComponent<ProjectileController>().attackController.AttackDamage = attackController.AttackDamage; 
                
                //projectile.GetComponent<ProjectileController>().direction = rotation2;

                // Set the projectile's parent to the ProjectileContainer
                if (projectileContainer != null)
                {
                    projectile.transform.SetParent(projectileContainer);
                }

                currentTimer = timeInterval; // Reset the timer
            }
        }
    }

    #endregion

    #region Public Methods

    public void OnPlayerDie()
    {
        MMEventManager.TriggerEvent(new EGameOver());
    }

    #endregion
}