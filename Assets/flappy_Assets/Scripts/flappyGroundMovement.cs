using UnityEngine;

public class flappyGroundMovement : MonoBehaviour
{
    public float life = 20f;
    public float speed;
    void Update()
    {
        life -= Time.deltaTime;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(0,0, speed*Time.deltaTime);
        }
    }
}
