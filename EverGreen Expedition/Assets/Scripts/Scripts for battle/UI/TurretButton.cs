using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TurretButton : MonoBehaviour , IPointerDownHandler
    {
        private Turret assignTurretPrefab;
        [SerializeField] private TextMeshProUGUI turretNameUI;
        [SerializeField] private TextMeshProUGUI waterCostUI;
        [SerializeField] private TextMeshProUGUI leafHandleCostUI;
        [SerializeField] private Image spriteImageContainer;
        public void Init(Turret assignedTurret)
        {
            assignTurretPrefab = assignedTurret;
            spriteImageContainer.sprite = assignTurretPrefab.TurretSprite;
            turretNameUI.text = assignTurretPrefab.TurretName;
            waterCostUI.text = assignTurretPrefab.WaterCost.ToString();
            leafHandleCostUI.text = assignTurretPrefab.LeafHandleCost.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(FightingEventManager.Instance.CanBuyTurret(
                assignTurretPrefab.WaterCost,
                assignTurretPrefab.LeafHandleCost
                ))
            {//if can buy, then player can dragging the turret
                ImageDragable.Instance.StartShowingMovingImage(assignTurretPrefab);
            }
            else
            {
                //show the error message here
            }
        }        
    }
}