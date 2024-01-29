using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaterCollectorTurret : Turret
    {
        [SerializeField] private int waterGain;
        [SerializeField] private int minAmountOfSecondToGatherWater;
        [SerializeField] private int maxAmountOfSecondToGatherWater;

        private void Start()
        {
            StartCoroutine(GainWater());
            EventManager.Instance.AddListener(TypeOfEvent.WinEvent, EndCoroutine);
            EventManager.Instance.AddListener(TypeOfEvent.LoseEvent, EndCoroutine);
        }

        private void EndCoroutine()
        {
            StopAllCoroutines();
        }

        private IEnumerator GainWater()
        {
            while (true)
            {
                var randomTime = Random.Range(minAmountOfSecondToGatherWater , maxAmountOfSecondToGatherWater);

                //show water here

                yield return new WaitForSeconds(randomTime);
                FightingEventManager.Instance.IncreaseWater(waterGain);
            }
        }
    }
}