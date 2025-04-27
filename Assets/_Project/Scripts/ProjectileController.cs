using MoreMountains.Tools;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 5f; // Speed of the projectile
    public Vector2 direction = Vector2.up; // Direction of the projectile

    #region MonoBehaviour

    void Update()
    {
        MoveProjectile();
        CheckIfOutsideScreen();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile collides with an enemy
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Die();
            
            MMEventManager.TriggerEvent(new EEarnScore()); // Trigger the score event
            Destroy(gameObject); // Destroy the projectile
        }
    }

    #endregion

    private void MoveProjectile()
    {
        // Move the projectile upward
        //transform.Translate(speed * Time.deltaTime * direction);
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    private void CheckIfOutsideScreen()
    {
        // Check if the projectile is outside the screen
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y > ConstValue.ViewportMaxY || 
            viewportPosition.x < ConstValue.ViewportMinX || 
            viewportPosition.x > ConstValue.ViewportMaxX || 
            viewportPosition.y < ConstValue.ViewportMinY)
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }

    
}