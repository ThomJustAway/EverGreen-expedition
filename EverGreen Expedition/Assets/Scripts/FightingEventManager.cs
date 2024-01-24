using Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FightingEventManager : Singleton<FightingEventManager>
    {
        //leaf handle
        private int maxLeafHandle;
        public int currentLeafHandle { get; private set; }
        public int waterResources { get; private set; }
        //hp
        private int maxHP;
        public int currentHP { get; private set; }

        public void StartNewGame()
        {
            //collect info from the game manager
            var stats = GameManager.Instance.playerStats;

            //reset the values
            currentHP = stats.maxHP;
            maxHP = stats.maxHP;

            currentLeafHandle = stats.maxLeafHandle;
            maxLeafHandle = stats.maxLeafHandle;
            //this can be changed if you want.
            waterResources = 0;
        }

        private void Start()
        {
            StartNewGame();
        }

        private void Update()
        {
            UIController.Instance.UpdateUI(maxHP, currentHP, maxLeafHandle, currentLeafHandle, waterResources);
        }

        public void IncreaseWater(int water)
        {
            waterResources += water;
        }

        public void TakeDamage(int damage)
        {
            currentHP -= damage;
            if(currentHP <= 0)
            {
                currentHP = 0;
                EventManager.Instance.AlertListeners(TypeOfEvent.LoseEvent);
            }
        }

        public bool CanBuyTurret(int waterCost, int leafHandleCost)
        {
            if(waterResources >= waterCost && //if the player has more or equal to the water cost
                currentLeafHandle >= leafHandleCost) //if the player has more or equal to the leaf handle cost
            {//buy that turret
                currentLeafHandle -= leafHandleCost;
                waterResources -= waterCost;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RefundLeafHandle(int leafHandle)
        {
            currentLeafHandle += leafHandle;
            if(currentLeafHandle > maxLeafHandle)
            {
                currentLeafHandle = maxLeafHandle;
            }
        }

        private void OnGUI()
        {
            bool TakeDamageButton = GUI.Button(new Rect(0, 0, 100, 100), "take damage");
            if (TakeDamageButton)
            {
                TakeDamage(100);
            }
            bool increaseWaterButton = GUI.Button(new Rect(100, 0, 100, 100), "increase water");
            if (increaseWaterButton)
            {
                IncreaseWater(10);
            }


        }

    }
}