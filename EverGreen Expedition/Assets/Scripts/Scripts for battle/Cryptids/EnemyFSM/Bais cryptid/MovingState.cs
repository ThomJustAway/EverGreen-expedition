﻿using PGGE.Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyFSM
{
    public class MovingState : CryptidBehaviourState<CryptidBehaviour>
    {
        private Transform player;

        public MovingState(FSM fSM, CryptidBehaviour cryptidBehaviour) : base(cryptidBehaviour , fSM)
        {
            mId = (int)EnemyState.move;
        }

        public override void Enter()
        {
            if(player == null) 
            { 
                player = GameObject.FindGameObjectWithTag(TagManager.playerTag).transform;
            }
        }


        public override void Update()
        {
            Transform cryptidTransform = cryptid.transform;
            Vector2 directionofMovement = player.position - cryptidTransform.position;
            directionofMovement.Normalize(); //now the cryptid knows where to go

            cryptidTransform.Translate(directionofMovement * Time.deltaTime * cryptid.MovementSpeed, Space.World);

            CanAttack();

        }

        private void CanAttack()
        {
            var hit = Physics2D.CircleCast(cryptid.transform.position,
                cryptid.AttackRadius,
                Vector2.zero,
                cryptid.AttackRadius,
                LayerMaskManager.TurretLayerMask);
            if (hit.collider != null) mFsm.SetCurrentState((int)EnemyState.attack);
        }
    }
}