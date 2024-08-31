using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMotionEffect : MonoBehaviour
{
    public RectTransform backgroundImage;
    public float speed = 10f;
    public float amplitude = 10f;

    private Vector2 originalPosition;

    void Start()
    {
        if (backgroundImage != null)
        {
            originalPosition = backgroundImage.anchoredPosition;
        }
    }

    void Update()
    {
        if (backgroundImage != null)
        {
            float xPosition = originalPosition.x + Mathf.Sin(Time.time * speed) * amplitude;
            float yPosition = originalPosition.y + Mathf.Cos(Time.time * speed) * amplitude;

            backgroundImage.anchoredPosition = new Vector2(xPosition, yPosition);
        }
    }
}
