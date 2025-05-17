using UnityEngine;

public class flappyPlayerMechanics : MonoBehaviour
{
    private float speed;
    public float target = 0.01f;
    public float jumpStrength = -0.01f;
    public float weight = 0.01f;
    public KeyCode jumpKey;
    public flappySoundSource soundSource;

    private float currentZRotation;
    private float targetZRotation = -100f;

    void Update()
    {
        transform.position = new Vector3(0.06200001f, transform.position.y, -2.6f);

        transform.Translate(speed, 0, 0);
        speed = Mathf.Lerp(speed, target, weight);

        if (Input.GetKeyDown(jumpKey))
        {
            speed = jumpStrength;
            soundSource.jumpsSFX();
            Debug.Log("Jump");
            currentZRotation = -60f;
        }

        currentZRotation = Mathf.Lerp(currentZRotation, targetZRotation, weight);
        transform.localEulerAngles = new Vector3(0, -90, currentZRotation);
    }
}