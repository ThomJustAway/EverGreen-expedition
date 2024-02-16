using Assets.Scripts.EnemyFSM;
using PGGE.Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Scripts_for_battle.Cryptids
{
    public class CryptidWormMovement : CryptidBehaviourState<CryptidWorm>
    {
        public CryptidWormMovement(CryptidWorm cryptidBehaviour, FSM fSM) : base(cryptidBehaviour, fSM)
        {
        }


    }
}