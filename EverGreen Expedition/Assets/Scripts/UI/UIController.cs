using Assets.Scripts;
using Assets.Scripts.UI;
using Patterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    private void Start()
    {
        healthBarWidth = healthBarRect.rect.width;
        SetUpTurretCard();
    }

    private void SetUpTurretCard()
    {
        Turret[] playerTurrets = GameManager.Instance.playerStats.turrets;
        foreach(var playerTurret in playerTurrets)
        {
            var card = Instantiate(turretCardPrefab , turretContainer);
            //initialise the button
            card.GetComponent<TurretButton>().Init(playerTurret);
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

}
