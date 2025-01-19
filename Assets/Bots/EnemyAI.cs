using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;   
    public Transform player;       
    public float detectionRange = 5f; 
    public float waypointTolerance = 1f; 

    private NavMeshAgent agent;       
    private int currentWaypointIndex = 0; 
    private bool isChasingPlayer = false; 

    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 6f; 
    }

    private void Update()
    {
        if (PlayerInRange())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        
        if (waypoints.Length == 0) return;

        
        if (!isChasingPlayer || agent.remainingDistance <= waypointTolerance)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

       
        if (agent.remainingDistance <= waypointTolerance && !agent.pathPending)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

      
        isChasingPlayer = false;
    }

    bool PlayerInRange()
    {
        // Check if player within range
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    void ChasePlayer()
    {
        
        agent.SetDestination(player.position);

       
        isChasingPlayer = true;
    }

    void OnDrawGizmos()
    {
        // Draw waypoints 
        if (waypoints != null && waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            foreach (Transform waypoint in waypoints)
            {
                if (waypoint != null)
                {
                    Gizmos.DrawSphere(waypoint.position, 0.3f);
                }
            }

            Gizmos.color = Color.blue;
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoints[i] != null && waypoints[(i + 1) % waypoints.Length] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
                }
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
