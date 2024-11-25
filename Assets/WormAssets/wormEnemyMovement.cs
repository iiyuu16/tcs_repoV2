using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class wormEnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 0.1f;
    public int seen = 0;

    private GameObject[] WPs;
    private NavMeshAgent agent;
    private int arrived = 1;
    private int chase = 0;
    //public turretAim turret;
    public GameObject barrel;

    public PlayerCollect collection;
    void Start()
    {
        WPs = GameObject.FindGameObjectsWithTag("Waypoint");
        agent = GetComponent<NavMeshAgent>();
        //turret = barrel.GetComponent<turretAim>();
        //turret.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (arrived == 1 && chase == 0)
        {
            target = WPs[Random.Range(0, WPs.Length)].transform;
            arrived = 0;
            StartCoroutine(FollowTarget());
            
        }
        else
        {
            //Debug.Log("EMPTY");
        }

        if (this.transform.position.x == target.transform.position.x && this.transform.position.z == target.transform.position.z)
        {
            arrived = 1;
            //Debug.Log("ARRIVED");
        }

        if(collection.timesHit == 5)
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(speed);
        agent.SetDestination(target.transform.position);
        //Debug.Log(target.gameObject.name);
        while (this.transform.position != target.transform.position)
        {
            yield return wait;

        }
        

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            //Debug.Log("PLAYER DETECTED");
            chase = 1;
            target = other.transform;
            seen = 1;
            barrel.SetActive(true);
            
            StartCoroutine(FollowTarget());
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            chase = 0;
            Debug.Log("CHASE OVER");
            barrel.SetActive(false);
            seen = 0;
        }
        
    }
}
