using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public Image panelImage;  // O Image do painel que exibir� a anima��o
    public Sprite[] sprites;  // Array de sprites que representam os frames da anima��o
    public float framesPerSecond = 10f;  // N�mero de frames por segundo

    private float timePerFrame;
    private float timeSinceLastFrame;
    private int currentFrameIndex = 0;

    void Start()
    {
        if (sprites.Length == 0 || panelImage == null)
        {
            Debug.LogError("Sprites ou Image n�o est�o configurados.");
            enabled = false;
            return;
        }

        timePerFrame = 1f / framesPerSecond;
    }

    void Update()
    {
        timeSinceLastFrame += Time.deltaTime;
        if (timeSinceLastFrame >= timePerFrame)
        {
            timeSinceLastFrame = 0f;
            currentFrameIndex = (currentFrameIndex + 1) % sprites.Length;
            panelImage.sprite = sprites[currentFrameIndex];
        }
    }
}
