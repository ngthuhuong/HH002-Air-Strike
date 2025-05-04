using UnityEngine;

public class ProjectileSeekerController : MonoBehaviour
{
    [Header("Seeker Settings")]
    [SerializeField] private float speed = 5f; // Speed of the projectile
    private Transform target; // Reference to the player's transform

    private void Start()
    {
        // Get the player's transform from the GamePlayManager
        if (GamePlayManager.Instance.Player != null)
        {
            target = GamePlayManager.Instance.Player.transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Move towards the player's position
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Optionally, rotate to face the player
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}