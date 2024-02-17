using Assets.Scripts.EnemyFSM;
using PGGE.Patterns;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets.Scripts.Scripts_for_battle.Cryptids
{
    public class CryptidWormMovement : CryptidBehaviourState<CryptidWorm>
    {
        private Transform player;


        public CryptidWormMovement(CryptidWorm cryptidBehaviour, FSM fSM) : base(cryptidBehaviour, fSM)
        {
        }

        public override void Enter()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag(TagManager.playerTag).transform;
            }
        }

        public override void Update()
        {
            Debug.Log("movement state");
            MoveCryptid();
            DecidingOnNextState();
        }

        private void MoveCryptid()
        {
            Transform cryptidTransform = cryptid.transform;
            Vector2 directionofMovement = player.position - cryptidTransform.position;
            directionofMovement.Normalize(); //now the cryptid knows where to go

            cryptidTransform.Translate(directionofMovement * Time.deltaTime * cryptid.MovementSpeed, Space.World);
        }

        private void DecidingOnNextState()
        {
            var hit = Physics2D.CircleCast(cryptid.transform.position,
                cryptid.AttackRadius,
                Vector2.zero,
                cryptid.AttackRadius,
                LayerMaskManager.TurretLayerMask);
            //if a raycast has been hit, it mean it can start attacking
            if (hit.collider != null) mFsm.SetCurrentState((int)EnemyState.attack);

            if (cryptid.CanDoBurrowThrow)
            {
                Debug.Log("row burrow throw");
                float randomValue = Random.value;
                if( randomValue <= cryptid.ProbabilityToBurrow)
                {//then do 
                    mFsm.SetCurrentState((int)EnemyState.burrow);
                }
            }

        }


    }
}