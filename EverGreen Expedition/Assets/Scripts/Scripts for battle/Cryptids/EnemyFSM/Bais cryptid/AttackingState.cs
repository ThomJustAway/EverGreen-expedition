using PGGE.Patterns;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets.Scripts.EnemyFSM
{
    public class AttackingState : CryptidBehaviourState<CryptidBehaviour>
    {
        private float elapseTime;

        public AttackingState( CryptidBehaviour cryptidBehaviour , FSM fSM) : base(cryptidBehaviour , fSM)
        {
            mId = (int)EnemyState.attack;
        }


        public override void Update()
        {
            if(elapseTime < cryptid.AttackSpeedPerSecond)
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
            var hit = Physics2D.CircleCast(cryptid.transform.position,
                cryptid.AttackRadius,
                Vector2.zero,
                cryptid.AttackRadius ,
                LayerMaskManager.TurretLayerMask
                );

            if (hit.collider == null)//there is nothing
            {
                mFsm.SetCurrentState((int) EnemyState.move);
            }
            else
            {
                cryptid.PlayAttackSoundEffect();
                var componentToHit = hit.transform.GetComponent<IDamageable>();
                componentToHit.TakeDamage(cryptid.Damage);
            }
        }
    }
}