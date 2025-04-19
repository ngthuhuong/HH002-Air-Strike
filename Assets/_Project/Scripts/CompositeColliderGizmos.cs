using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D))]
public class CompositeColliderGizmos : MonoBehaviour
{
    [SerializeField] private CompositeCollider2D compositeCollider;

    void OnDrawGizmosSelected()
    {
        // Ensure the CompositeCollider2D is initialized
        if (compositeCollider == null)
        {
            compositeCollider = GetComponent<CompositeCollider2D>();
        }

        if (compositeCollider != null)
        {
            Gizmos.color = Color.green; // Set the Gizmos color

            // Iterate through each path in the CompositeCollider2D
            for (int i = 0; i < compositeCollider.pathCount; i++)
            {
                Vector2[] points = new Vector2[compositeCollider.GetPathPointCount(i)];
                compositeCollider.GetPath(i, points);

                // Draw lines between the points in the path
                for (int j = 0; j < points.Length; j++)
                {
                    Vector2 start = points[j];
                    Vector2 end = points[(j + 1) % points.Length]; // Loop back to the first point
                    Gizmos.DrawLine(transform.TransformPoint(start), transform.TransformPoint(end));
                }
            }
        }
    }
}