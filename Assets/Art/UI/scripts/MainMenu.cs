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
        "O mundo que você conhecia não existe mais, transformado em uma vasta terra desolada cheia de perigos e oportunidades ocultas. Cada rua, cada edifício em ruínas, guarda segredos de uma era passada, esperando para serem desvendados. Mas cuidado, pois o que você encontrar pode ser tão perigoso quanto valioso. Em meio ao caos, você precisará explorar cada canto, vasculhar cada recanto sombrio e enfrentar criaturas que nunca deveriam ter existido. Suas descobertas não só poderão garantir sua sobrevivência, mas também revelar a verdadeira história por trás do fim do mundo.",
        "Neste novo mundo implacável, sobreviver requer mais do que apenas força bruta; é preciso inteligência, estratégia e a capacidade de tomar decisões rápidas sob pressão. A escassez de recursos significa que cada escolha importa, e um erro pode custar caro. Você precisará gerenciar cuidadosamente seu inventário, racionar alimentos e munições, e decidir em quem confiar. O tempo todo, perigos espreitam em cada esquina, e o menor movimento errado pode ser fatal. Prepare-se para um jogo mental onde cada passo pode ser o último, e onde a única constante é a luta pela sobrevivência.",
        "Em um mundo onde as regras foram quebradas e a sociedade desmoronou, sua identidade é tudo o que resta para distinguir você dos outros sobreviventes. Personalize seu personagem com um arsenal de roupas, armaduras e armas que não só aumentam suas chances de sobrevivência, mas também contam uma história única. Suas escolhas refletirão não apenas seu estilo de jogo, mas também a história que você deseja contar em meio a esse cenário devastado. Cada peça de equipamento, cada tatuagem ou cicatriz que você adquirir, será um testemunho da jornada que escolheu trilhar."
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
