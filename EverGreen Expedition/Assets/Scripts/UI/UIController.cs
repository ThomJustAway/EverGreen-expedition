using Patterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    // Start is called before the first frame update
    [SerializeField]private TextMeshProUGUI playerWaterResourcesText;
    [SerializeField] private TextMeshProUGUI playerLeafHandleResourcesText;

    [SerializeField] private TextMeshProUGUI healthPointText;
    [SerializeField] private RectTransform healthBarRect;
    private float healthBarWidth;

    private void Start()
    {
        healthBarWidth = healthBarRect.rect.width;
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
