using UnityEngine;

public class flappyPlayerMechanics : MonoBehaviour
{
    private float xSpeed;
    public float xTarget;
    public float jumpStr;
    public float weight;
    public KeyCode jumpKey;
    public flappySoundSource soundSource;

    void Update()
    {
        transform.Translate(xSpeed, 0, 0);
        xSpeed = Mathf.Lerp(xSpeed, xTarget, weight);

        if (Input.GetKeyDown(jumpKey))
        {
            xSpeed = jumpStr;
            soundSource.jumpsSFX();
            Debug.Log("jump");
        }
    }
}