using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("SCENES")]
    [SerializeField] private string playActionScene;
    [SerializeField] private string aboutActionScene;
    [SerializeField] private string settingsActionScene;

    [Header("SFX")]
    [Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
    public AudioSource hoverSound;
    [Tooltip("The GameObject holding the Audio Source component for the CLICK SOUND")]
    public AudioSource clickSound;
    [Tooltip("The Audio Source for the start game sound")]
    public AudioSource startGameSound;

    [Header("TEXT")]
    public TextMeshProUGUI miniText;
    private string[] tips = {
        "O mundo que voc� conhecia n�o existe mais, transformado em uma vasta terra desolada cheia de perigos e oportunidades ocultas. Cada rua, cada edif�cio em ru�nas, guarda segredos de uma era passada, esperando para serem desvendados. Mas cuidado, pois o que voc� encontrar pode ser t�o perigoso quanto valioso. Em meio ao caos, voc� precisar� explorar cada canto, vasculhar cada recanto sombrio e enfrentar criaturas que nunca deveriam ter existido. Suas descobertas n�o s� poder�o garantir sua sobreviv�ncia, mas tamb�m revelar a verdadeira hist�ria por tr�s do fim do mundo.",
        "Neste novo mundo implac�vel, sobreviver requer mais do que apenas for�a bruta; � preciso intelig�ncia, estrat�gia e a capacidade de tomar decis�es r�pidas sob press�o. A escassez de recursos significa que cada escolha importa, e um erro pode custar caro. Voc� precisar� gerenciar cuidadosamente seu invent�rio, racionar alimentos e muni��es, e decidir em quem confiar. O tempo todo, perigos espreitam em cada esquina, e o menor movimento errado pode ser fatal. Prepare-se para um jogo mental onde cada passo pode ser o �ltimo, e onde a �nica constante � a luta pela sobreviv�ncia.",
        "Em um mundo onde as regras foram quebradas e a sociedade desmoronou, sua identidade � tudo o que resta para distinguir voc� dos outros sobreviventes. Personalize seu personagem com um arsenal de roupas, armaduras e armas que n�o s� aumentam suas chances de sobreviv�ncia, mas tamb�m contam uma hist�ria �nica. Suas escolhas refletir�o n�o apenas seu estilo de jogo, mas tamb�m a hist�ria que voc� deseja contar em meio a esse cen�rio devastado. Cada pe�a de equipamento, cada tatuagem ou cicatriz que voc� adquirir, ser� um testemunho da jornada que escolheu trilhar."
    };

    [Header("Effect Panel")]
    public Image panelImage;
    public Color[] colors;
    public float cycleSpeed = 1f;

    private int currentColorIndex = 0;
    private float lerpTime = 0f;

    private int currentTip = 0;

    [Header("LOADING SCREEN")]
    [Tooltip("If this is true, the loaded scene won't load until receiving user input")]
    public bool waitForInput = true;

    [Header("Fade Effect")]
    public Image panelFade;
    public Color[] colorsFade;
    public float fadeDuration = 1f;
    public float volumeFadeDuration = 1f;

    void Start()
    {
        InvokeRepeating("ChangeMiniText", 0, 5f);
    }

    void Update()
    {
        if (colors.Length == 0 || panelImage == null)
            return;

        lerpTime += Time.deltaTime * cycleSpeed;
        if (lerpTime > 1f)
        {
            lerpTime = 0f;
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }

        int nextColorIndex = (currentColorIndex + 1) % colors.Length;
        panelImage.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], lerpTime);
    }

    void ChangeMiniText()
    {
        miniText.text = tips[currentTip];
        currentTip = (currentTip + 1) % tips.Length;
    }

    public void StartGame()
    {
        startGameSound.Play();

        StartCoroutine(FadeToBlackAndLoadScene());
    }

    private IEnumerator FadeToBlackAndLoadScene()
    {
        if (panelFade == null)
            yield break;

        float fadeDuration = 6f; 
        Color initialColor = panelFade.color; 
        Color targetColor = Color.black; 

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / fadeDuration;
            panelFade.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        panelFade.color = targetColor;

        SceneManager.LoadScene(playActionScene);
    }


    public void AboutAction()
    {
        SceneManager.LoadScene(aboutActionScene);
    }

    public void SettingsAction()
    {
        SceneManager.LoadScene(settingsActionScene);
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlayClick()
    {
        clickSound.Play();
    }

    IEnumerator LoadAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void LoadScene(string scene)
    {
        if (!string.IsNullOrEmpty(scene))
        {
            StartCoroutine(LoadAsynchronously(scene));
        }
    }
}
