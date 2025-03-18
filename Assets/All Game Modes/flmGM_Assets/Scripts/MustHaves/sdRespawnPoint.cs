using UnityEngine;

public class sdRespawnPoint : MonoBehaviour
{
    public Transform respawnPoint;
    public sdPlayerMovement _sdPlayerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with border");

            other.transform.position = respawnPoint.position;
            other.transform.rotation = respawnPoint.rotation;

            _sdPlayerMovement.currentSpeed = 0;
        }
    }
}
