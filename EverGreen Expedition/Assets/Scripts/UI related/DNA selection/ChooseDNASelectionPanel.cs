using Assets.Scripts;
using Assets.Scripts.Data_manager;
using Assets.Scripts.UI_related.DNA_selection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseDNASelectionPanel : MonoBehaviour
{
    //create the prefab
    [SerializeField] private GameObject AddDNACardPrefab;
    [SerializeField] private Turret[] turretAvaliable;
    [SerializeField] private Transform cardPanel;
    [SerializeField] private TextMeshProUGUI detailText;
    [SerializeField] private GameObject continueButton;

    private Turret selectedTurret;

    private void Start()
    {
        detailText.text = "";

        var turretPlayerHas = GameManager.Instance.playerStats.turrets;

        var turretToSelect = new Queue<Turret>();
        foreach (var turret in turretAvaliable)
        {
            if (!turretPlayerHas.Contains(turret))
            {
                turretToSelect.Enqueue(turret);
            }
        }
        for(int i = 0; i < 3; i++)
        {
            if (turretToSelect.TryDequeue(out Turret turretChosen))
            {
                GameObject card = Instantiate(AddDNACardPrefab, cardPanel);
                DNACard component = card.GetComponent<DNACard>();
                component.Init(this, turretChosen);
            }
        }
    }

    //when player choose the continue button
    public void ChooseDNA()
    {
        if(selectedTurret == null)
        {
            Debug.LogError("Error: no turret selected");
            return;
        }
        GameManager.Instance.playerStats.turrets.Add(selectedTurret);
        SceneManager.LoadScene(SceneName.MainLevel);
    }

    

    public void SetTurret(Turret turret)
    {
        selectedTurret = turret;
        detailText.text ="<b>Detail:</b>" + turret.Detail;
        continueButton.SetActive(true);
    }
}
