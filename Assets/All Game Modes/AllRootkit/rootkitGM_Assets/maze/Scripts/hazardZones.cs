using UnityEngine;
using System.Collections;

public class hazardZones : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 3f;

    public enum MovementType { Vertical, Horizontal }
    public MovementType movementType = MovementType.Vertical;

    private void Start()
    {
        if (movementType == MovementType.Vertical)
            StartCoroutine(VerticalMovement());
        else
            StartCoroutine(HorizontalMovement());
    }

    IEnumerator VerticalMovement()
    {
        while (true)
        {
            // Move from A to B
            yield return MoveToPoint(pointB.position);
            yield return new WaitForSeconds(waitTime);

            // Move from B to A
            yield return MoveToPoint(pointA.position);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator HorizontalMovement()
    {
        while (true)
        {
            // Move from A to B
            yield return MoveToPoint(pointB.position);
            yield return new WaitForSeconds(waitTime);

            // Move from B to A
            yield return MoveToPoint(pointA.position);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToPoint(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target; // Snap to target
    }
}
