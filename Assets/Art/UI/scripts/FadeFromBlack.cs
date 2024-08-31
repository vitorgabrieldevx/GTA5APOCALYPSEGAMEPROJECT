using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class FadeFromBlack : MonoBehaviour
{
    public Image panelFade;
    public string sceneToLoad;

    void Start()
    {
        if (panelFade != null)
        {
            panelFade.color = Color.black;
            StartCoroutine(FadeFromBlackCoroutine());
        }
    }

    private IEnumerator FadeFromBlackCoroutine()
    {
        float fadeDuration = 10f;
        Color initialColor = Color.black;
        Color targetColor = new Color(0, 0, 0, 0);

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / fadeDuration;
            panelFade.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        panelFade.color = targetColor;

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
