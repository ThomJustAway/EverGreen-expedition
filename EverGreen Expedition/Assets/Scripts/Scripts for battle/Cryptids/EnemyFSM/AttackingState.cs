using PGGE.Patterns;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets.Scripts.EnemyFSM
{
    public class AttackingState : CryptidState
    {
        private float elapseTime;

        public AttackingState( CryptidBehaviour cryptidBehaviour , FSM fSM) : base(cryptidBehaviour , fSM)
        {
            mId = (int)EnemyState.attack;
        }


        public override void Update()
        {
            if(elapseTime < cryptidBehaviour.AttackSpeedPerSecond)
            {
                elapseTime += Time.deltaTime;
            }
            else
            {
                AttackObject();
                elapseTime = 0;
            }
        }

        public override void Exit()
        {
            elapseTime = 0;
            Debug.Log("exiting attack state");
        }

        private void AttackObject()
        {
            var hit = Physics2D.CircleCast(cryptidBehaviour.transform.position,
                cryptidBehaviour.AttackRadius,
                Vector2.zero,
                cryptidBehaviour.AttackRadius ,
                LayerMaskManager.TurretLayerMask
                );

            if (hit.collider == null)//there is nothing
            {
                mFsm.SetCurrentState((int) EnemyState.move);
            }
            else
            {
                cryptidBehaviour.PlayAttackSoundEffect();
                var componentToHit = hit.transform.GetComponent<IDamageable>();
                componentToHit.TakeDamage(cryptidBehaviour.Damage);
            }
        }
    }
}