using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootController : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform projectileSpawnPoint; 
    [SerializeField] private Transform projectileContainer; 
    [SerializeField] private float shootingInterval = 0.5f;

    private float currentTimer = 0f;
    
    [Header("Stats")]
    [SerializeField] private int projectileDamage = 2; 

    private void Start()
    {
        projectileContainer = GamePlayManager.Instance.enemyProjectileContainer.transform;
    }

    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        currentTimer -= Time.deltaTime;

        // Check if the player presses the shoot button and the timer has reached zero
        if (currentTimer <= 0f)
        {
            ShootProjectile();
            currentTimer = shootingInterval;
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            //GameObject projectile = PoolingManager.Instance.GetObject(PoolingManager.PoolTag.EnemyProjectile, projectileSpawnPoint.position, Quaternion.identity);
            
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            // Set the projectile's parent to the container (if provided)
            if (projectileContainer != null)
            {
                projectile.transform.SetParent(projectileContainer);
            }

            // Optionally, configure the projectile (e.g., set direction, speed, etc.)
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            if (projectileController != null)
            {
                projectileController.moveController.Direction = Vector2.down;
                projectileController.attackController.AttackDamage = projectileDamage;
            }
        }
    }
}