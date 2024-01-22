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
        [SerializeField] private Turret assignTurretPrefab;
        [SerializeField] private TextMeshProUGUI turretNameUI;
        [SerializeField] private TextMeshProUGUI waterCostUI;
        [SerializeField] private TextMeshProUGUI leafHandleCostUI;
        [SerializeField] private Image spriteImageContainer;
        public void Init()
        {
            spriteImageContainer.sprite = assignTurretPrefab.TurretSprite;
            turretNameUI.text = assignTurretPrefab.TurretName;
            waterCostUI.text = assignTurretPrefab.WaterCost.ToString();
            leafHandleCostUI.text = assignTurretPrefab.LeafHandleCost.ToString();

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ImageDragable.Instance.StartShowingMovingImage(assignTurretPrefab);
        }        
    }
}