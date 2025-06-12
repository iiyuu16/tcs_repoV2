using UnityEngine;

public class CursorPlayer : MonoBehaviour
{
    public Transform startPoint;
    public mazeConditionManager _mazeConditionManager;
    public mazeSoundSource soundSource;
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
            soundSource.deadSFX();
            ResetToStart();
            isDragging = false;
        }
        else if (other.CompareTag("Hazard") && isDragging)
        {
            Debug.LogWarning("Hit hazard while moving! Resetting.");
            soundSource.deadSFX();
            ResetToStart();
            isDragging = false;
        }
        else if (other.CompareTag("Target"))
        {
            Debug.Log("Target reached!");
            if (_mazeConditionManager != null)
            {
                _mazeConditionManager.mazeWin();
            }
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
}
