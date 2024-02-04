using Assets.Scripts.UI_related;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class selectingCharacterButton : MonoBehaviour
{
    [SerializeField] private CharacterSelectionPanel panel;

    [SerializeField] private PlayerCurrentFernWeaverStats stats;
    [SerializeField] private Image characterProfile;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI leafHandle;

    private void Start()
    {
        characterProfile.sprite = stats.fernWeaverSprite;

        nameText.text = $"Name: <color=#63ADEE><b>{stats.name}";
        health.text = $"HP: <color=#F46969>{stats.maxHP}";
        leafHandle.text = $"Leaf Handle: <color=#C3D349><b>{stats.maxLeafHandle}<sprite=0>";
    }

    public void OnClick()
    {
        panel.SetStat(stats);
    }
}
