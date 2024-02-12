using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI_related.DNA_selection
{
    public class DNACard : MonoBehaviour
    {
        private ChooseDNASelectionPanel referencePanel;
        private Turret assignTurret;
        [SerializeField] private Image turretImage;
        [SerializeField]private TextMeshProUGUI turretNameText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI leafHandleText;
        [SerializeField] private TextMeshProUGUI waterHandleText;

        public void Init(
            ChooseDNASelectionPanel referencePanel , 
            Turret turret
            )
        {
            this.referencePanel = referencePanel;
            assignTurret = turret;

            turretImage.sprite = turret.TurretSprite;
            turretNameText.text = turret.TurretName;
            healthText.text = $"<color=#F46969>{turret.HealthPoint}<sprite=0>";
            leafHandleText.text = $"<color=#C3D349><b>{turret.LeafHandleCost}<sprite=0>";
            waterHandleText.text = $"<color=#1BC6FF><b>{turret.WaterCost}<sprite=0>";


        }

        public void OnClick()
        {
            referencePanel.SetTurret(assignTurret);
        }
    }
}