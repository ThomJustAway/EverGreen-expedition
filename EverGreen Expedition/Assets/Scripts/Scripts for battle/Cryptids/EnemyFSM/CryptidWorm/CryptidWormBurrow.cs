using Assets.Scripts.EnemyFSM;
using PGGE.Patterns;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets.Scripts.Scripts_for_battle.Cryptids
{
    public class CryptidWormBurrow : CryptidBehaviourState<CryptidWorm>
    {
        private float timeToBurrow;
        private Transform player;
        private float elapseTime;
        private bool hasFoundTarget;
        private bool hasStartedExit;
        private bool hasStartedMoving;
        public CryptidWormBurrow(CryptidWorm cryptid, FSM fSM) : base(cryptid, fSM)
        {
        }

        public override void Enter()
        {
            //decide how long to burrow
            timeToBurrow = Random.Range(cryptid.MinAmountOfTimeToBurrow , cryptid.MaxAmountOfTimeToBurrow);
            elapseTime = 0f;
            hasFoundTarget = false;
            hasStartedExit = false;
            hasStartedMoving = false;
            //show the cryptid spike location
            cryptid.ShowSpikeLocation();
            cryptid.HideWorm();

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag(TagManager.playerTag).transform;
            }


        }

        public override void Update()
        {

            //has not hide the cryptid
            if (!hasStartedMoving)
            {
                if (!cryptid.hasHide ) 
                { 
                    return; 
                } //wait for it to hide
                else
                {
                    hasStartedMoving = true;
                }
            }
            

            if(elapseTime < timeToBurrow && 
                !hasFoundTarget )
            {
                MoveCryptid();
                CheckTurret();
                elapseTime += Time.deltaTime;
            }
            else
            {
                if(!hasStartedExit)
                {
                    DoExitFromBurrow();
                    return;
                }
                if (!cryptid.hasHide)
                { //that means that the cryptid has fully show itself.
                    DecideOnState();
                }
            }
        }

        private void DecideOnState()
        {
            var hit = Physics2D.CircleCast(cryptid.transform.position,
                                    cryptid.AttackRadius,
                                    Vector2.zero,
                                    cryptid.AttackRadius,
                                    LayerMaskManager.TurretLayerMask);
            if (hit)
            {
                mFsm.SetCurrentState((int)EnemyState.attack);
            } //then do attack state
            else
            {
                mFsm.SetCurrentState((int)EnemyState.move);
                //then do walking animation
            }
        }

        public void DoExitFromBurrow()
        {
            hasStartedExit = true;
            cryptid.HideSpikeLocation();
            cryptid.ShowWorm();
        }

        private void CheckTurret()
        {
            var hit = Physics2D.CircleCast(cryptid.transform.position,
                cryptid.AttackRadius,
                Vector2.zero,
                cryptid.AttackRadius,
                LayerMaskManager.TurretLayerMask);
            if (hit.collider != null) hasFoundTarget = true;
        }

        private void MoveCryptid()
        {
            Transform cryptidTransform = cryptid.transform;
            Vector2 directionofMovement = player.position - cryptidTransform.position;
            directionofMovement.Normalize(); //now the cryptid knows where to go

            cryptidTransform.Translate(directionofMovement * Time.deltaTime * cryptid.BurrowingSpeed, Space.World);
        }

         

    }
}