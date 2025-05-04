using System;
using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private MoveBehaviour moveBehaviour = MoveBehaviour.Straight;
    
    [Header("Stats")]
    [SerializeField] private float speed = 5f; 

    [Header("Straight Settings")]
    [SerializeField] private Vector2 direction = Vector2.zero;
    public Vector2 Direction
    {
        get => direction;
        set => direction = value.normalized;
    }
    

    [Header("Seeking Settings")]
    [SerializeField] private float seekingDuration = 15f; 
    private float seekingTimer = 0f;
    [SerializeField] private Transform targetTransform; // Target for seeking or target-based movement
    [SerializeField] private Vector3 offset = Vector3.zero; 
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public Vector3 Offset { get => offset; set => offset = value; }
    
    
    [Header("Circular Settings")]
    public Transform centerPoint; 
    [SerializeField] private float radius = 2f; 
    [SerializeField] private float rotationSpeed = 2f; 
    [SerializeField] private float movementDuration = 999f;
    private IEnumerator currentCoroutine;
    
    private enum MoveBehaviour
    {
        Straight,
        Target,
        Cicular
    }

    #region MonoBehaviour

    private void Start()
    {
        // Setup for Seeking
        targetTransform = GamePlayManager.Instance.Player.transform;
        seekingTimer = 0f;
        centerPoint = GamePlayManager.Instance.centerPoint;

        if (moveBehaviour == MoveBehaviour.Cicular)
        {
            Debug.Log("Start Circular move");
            currentCoroutine = MoveAroundCenter();
            StartCoroutine(currentCoroutine);

        }
    }

    private void Update()
    {
        switch (moveBehaviour)
        {
            case MoveBehaviour.Straight:
                MoveInDirection();
                break;
            case MoveBehaviour.Target:
                HandleSeeking();
                break;
            case MoveBehaviour.Cicular:
                MoveInDirection();
                break;
        }
    }

    #endregion

    #region Public Methods

    // Set the target position for movement
    public void SetTargetPosition(Transform newTargetPosition, bool enableSeeking = false, Vector3 newOffset = default)
    {
        targetTransform = newTargetPosition;
        offset = newOffset;
        seekingTimer = 0f; // Reset seeking timer
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


    // Handle seeking behavior
    private void HandleSeeking()
    {
        if (targetTransform != null)
        {
            seekingTimer += Time.deltaTime;

            if (seekingTimer > seekingDuration)
            {
                moveBehaviour = MoveBehaviour.Straight;
                StopCoroutine(currentCoroutine);
                return;
            }

            Vector3 targetPositionWithOffset = targetTransform.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPositionWithOffset, speed * Time.deltaTime);

            // Optionally, rotate to face the target
            direction = (targetPositionWithOffset - transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private IEnumerator MoveAroundCenter()
    {
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the angle and direction for circular movement
            float angle = elapsedTime * rotationSpeed;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 targetPosition = (Vector2)centerPoint.position + offset;

            // Set the direction in MoveController
            direction = (targetPosition - (Vector2)transform.position).normalized;

            yield return null;
        }

        // Stop movement after the duration
        // direction = Vector2.zero;
    }
    
    #endregion
}