using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 3f; // Speed of the enemy movement

    #region MonoBehaviour

    void Update()
    {
        // Move the enemy downward
        transform.position += speed * Time.deltaTime * Vector3.down;
        
        CheckIfOutsideScreen();
    }

    #endregion

    #region Private Methods

    private void CheckIfOutsideScreen()
    {
        // Check if the projectile is outside the screen
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y < 0)
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }

    #endregion
}