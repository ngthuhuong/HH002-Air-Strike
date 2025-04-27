using MoreMountains.Tools;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Components")] 
    public AttackController attackController;
    
    
    [Header("Stats")]
    public float speed = 5f; // Speed of the projectile
    public Vector2 direction = Vector2.up; // Direction of the projectile

    #region MonoBehaviour

    void Update()
    {
        MoveProjectile();
        CheckIfOutsideScreen();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Enemy))
        {
            HealthController enemy = other.GetComponent<HealthController>();
            if (enemy != null)
            {
                Debug.Log($"Enemy takes damage: {attackController.AttackDamage}");
                enemy.TakeDamage(attackController.AttackDamage);
                Destroy(gameObject); // Destroy the projectile
            }
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