﻿using Patterns;
using System.Collections;
using UnityEngine;
using EventManagerYC;
using System;
using Unity.Mathematics;

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

        public int experienceGain { get; private set; }
        public int cryptidRemainGain { get; private set; }

        #region water
        private int waterGainPerSecond;
        [SerializeField] private float minTimeTakenToCollectWater;
        [SerializeField] private float maxTimeTakenCollectWater;
        #endregion
        private void Start()
        {
            StartNewGame();
        }

        private void Update()
        {
            UIController.Instance.UpdateUI(maxHP, currentHP, maxLeafHandle, currentLeafHandle, waterResources);
        }


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

            experienceGain = 0;
            cryptidRemainGain = 0;

            waterGainPerSecond = stats.waterPerSecond;

            StartCoroutine(GraduallyIncreaseWater());

            //EventManager.Instance.CryptidDeathAddListener(GainExpAndRemain);
            EventManager.Instance.AddListener(TypeOfEvent.CryptidDeath, (Action<CryptidBehaviour>) GainExpAndRemain );
            EventManager.Instance.AddListener(TypeOfEvent.WinEvent, UpdateWinScreen);
            EventManager.Instance.AddListener(TypeOfEvent.WinEvent, RemoveDependecy);
            EventManager.Instance.AddListener(TypeOfEvent.LoseEvent, RemoveDependecy);
        }

        private IEnumerator GraduallyIncreaseWater()
        {
            while (true)
            {
                IncreaseWater(waterGainPerSecond);
                float newTimeToWait = UnityEngine.Random.Range(minTimeTakenToCollectWater, maxTimeTakenCollectWater);
                yield return new WaitForSeconds(newTimeToWait); 
            }
        }

        public void IncreaseWater(int water)
        {
            SoundManager.Instance.PlayAudio(SFXClip.WaterIncreaseing);
            waterResources += water;
        }

        public void TakeDamage(int damage)
        {
            currentHP -= damage;
            if(currentHP <= 0)
            {
                currentHP = 0;
                EventManager.Instance.TriggerEvent(TypeOfEvent.LoseEvent);
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

        #region cryptid death experience
        private void GainExpAndRemain(CryptidBehaviour enemy)
        {
            //gain the experience
            cryptidRemainGain += UnityEngine.Random.Range(enemy.CryptidRemainMin, enemy.CryptidRemainMax);
            experienceGain += UnityEngine.Random.Range(enemy.MinExp, enemy.MaxExp);
        }
        #endregion

        #region win event action

        private void UpdateWinScreen()
        {
            UIController.Instance.UpdateWinScreen(experienceGain, cryptidRemainGain);
        }

        private void RemoveDependecy()
        {
            EventManager.Instance.RemoveListener(TypeOfEvent.CryptidDeath, (Action<CryptidBehaviour>) GainExpAndRemain);
            EventManager.Instance.RemoveListener(TypeOfEvent.WinEvent, UpdateWinScreen);
            EventManager.Instance.RemoveListener(TypeOfEvent.WinEvent , RemoveDependecy);
            EventManager.Instance.RemoveListener(TypeOfEvent.LoseEvent, RemoveDependecy);
        }

        #endregion
        public void RefundLeafHandle(int leafHandle)
        {
            currentLeafHandle += leafHandle;
            if(currentLeafHandle > maxLeafHandle)
            {
                currentLeafHandle = maxLeafHandle;
            }
        }

        public void OnWinContinueClick()
        {
            GameManager.Instance.UpdateStatsOnWin(cryptidRemainGain, experienceGain);
        }

        //private void OnGUI()
        //{
        //    bool TakeDamageButton = GUI.Button(new Rect(0, 0, 100, 100), "take damage");
        //    if (TakeDamageButton)
        //    {
        //        TakeDamage(100);
        //    }
        //    bool increaseWaterButton = GUI.Button(new Rect(100, 0, 100, 100), "increase water");
        //    if (increaseWaterButton)
        //    {
        //        IncreaseWater(10);
        //    }


        //}

    }
}