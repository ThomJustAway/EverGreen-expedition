using Assets.Scripts.EnemyFSM;
using EventManagerYC;
using PGGE.Patterns;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scripts_for_battle.Cryptids
{
    public class CryptidWorm : CryptidBehaviour
    {
        [Header("Burrow")]
        [SerializeField] private float burrowingSpeed;
        public float BurrowingSpeed { get { return burrowingSpeed; } }
        [SerializeField] private float minAmountOfTimeToBurrow;
        public float MinAmountOfTimeToBurrow { get {  return minAmountOfTimeToBurrow; } }
        [SerializeField] private float maxAmountOfTimeToBurrow;
        public float MaxAmountOfTimeToBurrow { get { return maxAmountOfTimeToBurrow; } }

        [SerializeField] private float timeTakenToHide;
        [Range(0,1f)]
        [SerializeField] private float probabilityToBurrow;
        public float ProbabilityToBurrow { get {  return  probabilityToBurrow; } }

        [SerializeField] private GameObject Spike; //this is the spike to show the location of the worm when burrowed
        [SerializeField] private Canvas spriteCanvas; //to show the spike and the sprite
        [SerializeField] private Slider spriteSlider;

        //[SerializeField] private float minTimeBurrow;
        //public float MinTimeBurrow { get { return minTimeBurrow; } }
        //[SerializeField] private float maxTimeBurrow;
        //public float MaxTimeBurrow { get { return maxTimeBurrow; } }

        [HideInInspector] public bool hasHide { get; private set; }

        private bool canBurrowThrow;

        public bool CanDoBurrowThrow
        {
            get 
            {
                if (canBurrowThrow)
                {
                    canBurrowThrow = false;
                    StartCoroutine(ResetValue(2f));
                    //show that it can do burrow throw
                    return true;
                    //do coroutine to return back to true
                }
                return false;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            hasHide = false;
            spriteCanvas.worldCamera = Camera.main;
        }


        protected override void SettingUpFSM()
        {
            canBurrowThrow = true;
            fsm = new FSM();
            fsm.Add((int)EnemyState.move, new CryptidWormMovement(this, fsm));
            fsm.Add((int)EnemyState.attack, new AttackingState(this, fsm));
            fsm.Add((int)EnemyState.burrow ,  new CryptidWormBurrow(this,fsm));
            fsm.SetCurrentState((int)EnemyState.move);
        }


        public void ShowSpikeLocation()
        {
            Spike.SetActive(true);
        }

        public void HideSpikeLocation()
        {
            Spike.SetActive(false);
        }

        public void HideWorm()
        {
            StartCoroutine(ChangeSliderValue(1f, 0f));
        }

        public void ShowWorm()
        {
            StartCoroutine(ChangeSliderValue(0, 1f));
        }

        private IEnumerator ChangeSliderValue(float initialValue , float finalValue)
        {
            float elapseTime = 0f;
            while (elapseTime < timeTakenToHide)
            {
                spriteSlider.value = Mathf.Lerp(initialValue, finalValue, elapseTime / timeTakenToHide);
                elapseTime += Time.deltaTime;
                yield return null;
            }
            spriteSlider.value = finalValue;


            if(!hasHide)
            {//has not hide, then hide it

                hasHide = true;
                cryptidCollider.enabled = false;
            }
            else
            {
                hasHide = false;
                cryptidCollider.enabled = true;
            }
        }

        private IEnumerator ResetValue(float time)
        {
            yield return new WaitForSeconds(time);
            canBurrowThrow = true;
        }

        protected override IEnumerator DeathCoroutine()
        {
            deathMusicClip.source.Play();
            spriteSlider.value = 0f;
            cryptidCollider.enabled = false;
            yield return new WaitForSeconds(1.4f); //wait for the death animation ends
            Destroy(gameObject);
        }
    }
}