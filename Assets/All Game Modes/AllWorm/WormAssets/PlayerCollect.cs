using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public int collected = 0;
    public int timesHit = 0;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("COLLIDED WITH SOMETHING");
        if(other.gameObject.tag == "Collectible" && collected == 0)
        {
            collected = 1;
            Debug.Log("COLLECTED");
        }

        if (collected == 1)
        {
            if (other.gameObject.tag == "Enemy")
            {
                collected = 0;
                audio.Play();
                timesHit++;
                if (timesHit > 4)
                {
                    other.gameObject.SetActive(false);
                    Debug.Log("ENEMY DOWN RIP BOZO PEACE OUT YALL");

                    //Win Condition
                }

                Debug.Log("HIT ENEMY");
            }
        }
    }


}
