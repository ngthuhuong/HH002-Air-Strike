using UnityEngine;
using UnityEngine.Serialization;

public class MoveController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private Vector2 direction = Vector2.zero; 
    public Vector2 Direction { get => direction; set => direction = value; }
    
    [SerializeField] private Transform targetTranform; 
    
    [Header("Flags")]
    [SerializeField] private bool moveTowardsTarget = false; // Flag to enable target-based movement


    #region MonoBehaviour

    void Update()
    {
        if (moveTowardsTarget)
        {
            MoveTowardsTarget();
        }
        else
        {
            MoveInDirection();
        }
    }

    #endregion

    #region Public Methods

    // Set the movement direction
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    // Set the target position for movement
    public void SetTargetPosition(Transform newTargetPosition)
    {
        targetTranform = newTargetPosition;
        moveTowardsTarget = true;
    }

    #endregion

    #region Private Methods

    // Move the object in the specified direction
    private void MoveInDirection()
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }

    // Move the object towards the target position
    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTranform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetTranform.position) < 0.1f)
        {
            moveTowardsTarget = false; // Stop moving when close to the target
        }
    }

    #endregion
}