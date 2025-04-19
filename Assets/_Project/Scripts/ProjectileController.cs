using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 5f; // Speed of the projectile

    #region MonoBehaviour

    void Update()
    {
        MoveProjectile();
        CheckIfOutsideScreen();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collider with {collision.name}");
        // Check if the projectile collides with an enemy
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            Destroy(enemy.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the projectile
        }
    }

    #endregion

    private void MoveProjectile()
    {
        // Move the projectile upward
        transform.position += speed * Time.deltaTime * Vector3.up;
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