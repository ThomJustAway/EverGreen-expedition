using EventManagerYC;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private bool canBuy = true;
        [SerializeField] private Slider sliderForCoolDown;

        public void Init(Turret assignedTurret)
        {
            assignTurretPrefab = assignedTurret;
            spriteImageContainer.sprite = assignTurretPrefab.TurretSprite;
            turretNameUI.text = assignTurretPrefab.TurretName;
            waterCostUI.text = assignTurretPrefab.WaterCost.ToString();
            leafHandleCostUI.text = assignTurretPrefab.LeafHandleCost.ToString();
        }

        private IEnumerator StartCoolDown()
        {
            canBuy = false; //player cant buy during this time

            float timeToWait = assignTurretPrefab.SpawnReload;
            float elapseTime = 0f;
            sliderForCoolDown.value = 1; //start at 1 and reduce it 
            while (timeToWait > elapseTime)
            {
                //slowly show that the cool down is coming to an end
                elapseTime += Time.deltaTime;
                float percentage = elapseTime / timeToWait;
                sliderForCoolDown.value = 1 - percentage;

                yield return new WaitForEndOfFrame(); 
            }

            sliderForCoolDown.value = 0;
            canBuy = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canBuy) 
            {
                string message = $"The turret is on cooldown!";
                int duration = 4;
                EventManager.Instance.TriggerEvent(TypeOfEvent.ShowPopUp, message, duration);
                return;
            }


            if(FightingEventManager.Instance.CanBuyTurret(
                assignTurretPrefab.WaterCost,
                assignTurretPrefab.LeafHandleCost
                ))
            {//if can buy, then player can dragging the turret
                StartCoroutine(StartCoolDown());
                ImageDragable.Instance.StartShowingMovingImage(assignTurretPrefab);
            }
            else
            {
                string message = $"You dont have enough resources!";
                int duration = 4;
                EventManager.Instance.TriggerEvent(TypeOfEvent.ShowPopUp, message, duration);
                //show error here
            }
        }        
    }
}