using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private MoveController moveController;

    [Header("Movement Settings")]
    public Transform centerPoint; // Center point for circular movement
    [SerializeField] private float radius = 2f; // Radius of the circular movement
    [SerializeField] private float rotationSpeed = 2f; // Speed of rotation
    [SerializeField] private float movementDuration = 999f; // Duration of the movement

    private bool isMoving = false;

    #region MonoBehaviour

    private void Start()
    {
        if (moveController != null)
        {
            StartCoroutine(MoveAroundCenter());
        }
    }

    private void OnDestroy()
    {
        MMEventManager.TriggerEvent(new EEnemyDie());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Player))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Private Methods

    private IEnumerator MoveAroundCenter()
    {
        isMoving = true;
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the angle and direction for circular movement
            float angle = elapsedTime * rotationSpeed;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 targetPosition = (Vector2)centerPoint.position + offset;

            // Set the direction in MoveController
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            moveController.Direction = direction;

            yield return null;
        }

        // Stop movement after the duration
        moveController.Direction = Vector2.zero;
        isMoving = false;
    }

    #endregion
}