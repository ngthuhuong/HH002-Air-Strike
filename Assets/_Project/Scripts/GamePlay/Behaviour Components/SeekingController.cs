using System;
using UnityEngine;

public class SeekingController : MonoBehaviour
{
    [Header("Seeking Settings")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float seekingDuration = 15f; 
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = Vector3.zero;

    private float seekingTimer = 0f;
    bool isSeeking = true; 

    public Transform Target { get => target; set => target = value; }
    public Vector3 Offset { get => offset; set => offset = value; }

    #region MonoBehaviour

    private void OnEnable()
    {
        target = GamePlayManager.Instance.Player.transform;
        seekingTimer = 0f; // Reset the timer when enabled
        isSeeking = true; // Enable seeking
    }

    private void Update()
    {
        if (isSeeking && target != null)
        {
            // Update the seeking timer
            seekingTimer += Time.deltaTime;

            // Stop seeking if the timer exceeds the duration
            if (seekingTimer > seekingDuration)
            {
                isSeeking = false;
                return;
            }

            // Calculate the target position with the offset
            Vector3 targetPositionWithOffset = target.position + offset;

            // Move towards the target's position with the offset
            transform.position = Vector3.MoveTowards(transform.position, targetPositionWithOffset, speed * Time.deltaTime);

            // Optionally, rotate to face the target
            Vector3 direction = (targetPositionWithOffset - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }

    #endregion
}