using PGGE.Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyFSM
{


    public class CryptidBehaviourState<TypeOfEnemy> : FSMState where TypeOfEnemy : CryptidBehaviour
    {
        protected TypeOfEnemy cryptid;

        public CryptidBehaviourState(TypeOfEnemy cryptid, FSM fSM) : base()
        {
            this.cryptid = cryptid;
            mFsm = fSM;
        }
    }  
}