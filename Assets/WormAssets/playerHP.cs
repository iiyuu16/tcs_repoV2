using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHP : MonoBehaviour
{
    public BoxCollider collide;
    public int HP;
    public GameObject healthbar;
    public Slider HealthBar;

    public Camera Camera;
    private CameraShake camshake;
    



    private int playerHealth = 10;

    // Start is called before the first frame update
    void Start()
    {

        //HealthBar = GetComponent<Slider>();
        collide = this.GetComponent<BoxCollider>();
        HealthBar.maxValue = HP;

        camshake = Camera.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            HP--;
            camshake.shakeDuration = 0.7f;
            if (HP < 1)
            {
                //Lose Condition
                this.gameObject.SetActive(false);
            }
            
            HealthBar.value = HP;
            //Debug.Log(HealthBar.value);
        }

        if (other.gameObject.tag == "Collectible")
        {
            HP = HP + 3;
        }
    }
}
