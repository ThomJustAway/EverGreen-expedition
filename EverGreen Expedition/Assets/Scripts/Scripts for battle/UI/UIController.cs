using Assets.Scripts.UI;
using EventManagerYC;
using TMPro;
using UnityEngine;

public class UIController : Patterns.Singleton<UIController>
{
    // Start is called before the first frame update
    [SerializeField]private TextMeshProUGUI playerWaterResourcesText;
    [SerializeField] private TextMeshProUGUI playerLeafHandleResourcesText;

    [SerializeField] private TextMeshProUGUI healthPointText;
    [SerializeField] private RectTransform healthBarRect;
    private float healthBarWidth;

    [Header("Turrets")]
    [SerializeField] private Transform turretContainer;
    [SerializeField] private GameObject turretCardPrefab;


    [Header("Enemies")]
    [SerializeField] private TextMeshProUGUI AmountOfEnemiesText;
    [SerializeField] private RectTransform waveProgress;
    private float waveWidthWave;

    [Header("win lose screen")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [Header("Win screen UI")]
    [SerializeField] private TextMeshProUGUI cryptidRemainGainText;
    [SerializeField] private TextMeshProUGUI experienceGainText;

    private void Start()
    {
        healthBarWidth = healthBarRect.rect.width;
        waveWidthWave = waveProgress.rect.width;
        SetUpTurretCard();

        EventManager.Instance.AddListener(TypeOfEvent.WinEvent, ShowWinScreen);
        EventManager.Instance.AddListener(TypeOfEvent.LoseEvent, ShowLoseScreen);
    }

    private void SetUpTurretCard()
    {
        var playerTurrets = GameManager.Instance.playerStats.turrets;
        int keybind = 1;
        foreach(var playerTurret in playerTurrets)
        {
            var card = Instantiate(turretCardPrefab , turretContainer);
            //initialise the button
            card.GetComponent<TurretButton>().Init(playerTurret , keybind);
            keybind++;
        }
    }

    public void UpdateUI(int maxHP, int currentHP , int maxLeafHandle , int currentLeafHandle , int currentWater)
    {
        UpdateHp(currentHP, maxHP);
        UpdateLeafhandleUI(currentLeafHandle, maxLeafHandle);
        UpdateWaterUI(currentWater);
    }

    private void UpdateHp(int health, int maxHealth)
    {
        float normaliseValue = (float)health / (float)maxHealth;
        float currentUIHealth = Mathf.Lerp(0, healthBarWidth, normaliseValue);
        Vector2 sizeOfRect = healthBarRect.sizeDelta;
        sizeOfRect.x = currentUIHealth;
        healthBarRect.sizeDelta = sizeOfRect;

        healthPointText.text = $"{health}/{maxHealth}";
    }

    private void UpdateLeafhandleUI(int currentLeafHandle, int maxLeafHandle)
    {
        playerLeafHandleResourcesText.text = $"{currentLeafHandle}/{maxLeafHandle}";
    }

    private void UpdateWaterUI(int currentWater)
    {
        playerWaterResourcesText.text = currentWater.ToString();
    }

    #region enemies 
    public void UpdateAmountOfEnemies(int amount)
    {
        AmountOfEnemiesText.text = amount.ToString();
    }

    public void UpdateWaveProgressBar(float progressPercentage) //from 0 to 1
    {
        float currentWidth = waveWidthWave * ( 1 - progressPercentage);
        Vector2 sizeOfRect = waveProgress.sizeDelta;
        sizeOfRect.x = currentWidth;
        waveProgress.sizeDelta = sizeOfRect;
    }
    #endregion

    #region win lose 
    private void ShowWinScreen()
    {
        Time.timeScale = 0f; //stop the time
        winScreen.SetActive(true);
    }

    private void ShowLoseScreen()
    {
        Time.timeScale = 0f; //stop the time
        loseScreen.SetActive(true);
    }

    #endregion

    public void UpdateWinScreen(int experiencegain, int cryptidRemain)
    {
        experienceGainText.text = $"{experiencegain} <color=yellow>Exp";
        cryptidRemainGainText.text = $"{cryptidRemain} <sprite=0>";
    }
}
