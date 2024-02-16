using System.Collections;
using UnityEngine;
using EventManagerYC;

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

        public override void TakeDamage(int amountOfDamage)
        {
            healthpoint -= amountOfDamage;

            EventManager.Instance.TriggerEvent(TypeOfEvent.ShowDamagePopUp, (Vector2)transform.position, amountOfDamage);

            if (healthpoint <= 0)
            {
                healthpoint = 0;
                EndCoroutine();
                FightingEventManager.Instance.RefundLeafHandle(leafHandleCost);
                Destroy(gameObject);
            }
        }

        private void EndCoroutine()
        {
            StopAllCoroutines();
        }

        public override void RemoveTurret()
        {
            EventManager.Instance.RemoveListener(TypeOfEvent.WinEvent, EndCoroutine);
            EventManager.Instance.RemoveListener(TypeOfEvent.LoseEvent, EndCoroutine);
            EndCoroutine();
            base.RemoveTurret();
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