using UnityEngine;

public class CursorPlayer : MonoBehaviour
{
    public Transform startPoint;         // Assign start point in inspector

    private bool isDragging = false;
    private float floorY;

    void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
            floorY = startPoint.position.y;
            Debug.Log("Cursor spawned at start point.");
        }
        else
        {
            Debug.LogError("Start point not assigned.");
        }
    }

    void Update()
    {
        if (isDragging)
        {
            MoveWithCursor();
        }

        // Optional: If you want to cancel drag on mouse up anywhere
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void MoveWithCursor()
    {
        Plane movementPlane = new Plane(Vector3.up, new Vector3(0, floorY, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (movementPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = floorY;
            transform.position = hitPoint;
        }
    }

    private void OnMouseDown()
    {
        // When player object clicked, start dragging
        isDragging = true;
        Debug.Log("Player picked up.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && isDragging)
        {
            Debug.LogWarning("Hit wall! Resetting and dropping player.");
            ResetToStart();
            isDragging = false;
        }
        else if (other.CompareTag("Target"))
        {
            Debug.Log("🎉 Reached END POINT!");
            // TODO: add win logic
        }
    }

    void ResetToStart()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
            floorY = startPoint.position.y;
            Debug.Log("Player reset to start.");
        }
        else
        {
            Debug.LogError("Start point not assigned.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        if (startPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(startPoint.position, 0.15f);
        }
    }
}
