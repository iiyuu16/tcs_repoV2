using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBforUI : MonoBehaviour
{
    public float speed = 1.0f;
    public Image image;

    private Color currentColor;
    private Color targetColor;
    private float timer = 0.0f;

    void Start()
    {
        currentColor = image.color;
        targetColor = new Color(Random.value, Random.value, Random.value);
    }

    void Update()
    {
        timer += Time.deltaTime * speed;

        if (timer >= 1.0f)
        {
            timer = 0.0f;
            currentColor = targetColor;
            targetColor = new Color(Random.value, Random.value, Random.value);
        }

        image.color = Color.Lerp(currentColor, targetColor, timer);
    }
}
