using MoreMountains.Tools;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Components")] 
    public AttackController attackController;
    public MoveController moveController;
    public string targetTag = TagConst.Enemy;

    #region MonoBehaviour

    void Update()
    {
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


    #endregion
    
}