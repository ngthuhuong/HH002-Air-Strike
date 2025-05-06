using MoreMountains.Tools;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Components")] 
    public AttackController attackController;
    public MoveController moveController;

    public string targetTag = TagConst.Enemy;
    [SerializeField] private PoolingManager.PoolTag poolTag;
    
    #region MonoBehaviour

    void Update()
    {
        CheckIfOutsideScreen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(gameObject);
            // ProjectilePoolObject.Instance.ReturnObject(gameObject);
            // PoolingManager.Instance.ReturnObject(poolTag, gameObject);
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
            // ProjectilePoolObject.Instance.ReturnObject(gameObject);
            // PoolingManager.Instance.ReturnObject(poolTag, gameObject);
            Destroy(gameObject);
        }
    }

    #region Public Methods


    #endregion
    
}