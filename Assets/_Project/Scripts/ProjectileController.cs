using MoreMountains.Tools;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Components")] 
    public AttackController attackController;
    public string targetTag = TagConst.Enemy;
    
    
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
        if (other.CompareTag(targetTag))
        {
            Debug.Log($"{targetTag}, {other.gameObject.tag}");
            HealthController targetHealth = other.GetComponent<HealthController>();
            if (targetHealth != null)
            {
                Debug.Log($"Target takes damage: {attackController.AttackDamage}");
                targetHealth.TakeDamage(attackController.AttackDamage);
                Destroy(gameObject); // Destroy the projectile
            }
        }
        
    }

    #endregion

    private void MoveProjectile()
    {
        // Move the projectile upward
        //transform.Translate(speed * Time.deltaTime * direction);
        transform.Translate(speed * Time.deltaTime * direction);
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

    #region Public Methods

    public void SetDirection(Vector2 _direction)
    {
        direction = _direction;
    }

    #endregion
    
}