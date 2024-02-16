using PGGE.Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyFSM
{
    //public class CryptidState : FSMState
    //{
    //    protected CryptidBehaviour cryptidBehaviour;

    //    public CryptidState(CryptidBehaviour cryptidBehaviour , FSM fSM): base()
    //    {
    //        this.cryptidBehaviour = cryptidBehaviour;
    //        mFsm = fSM;
    //    }
    //}

    public class CryptidBehaviourState<TypeOfEnemy> : FSMState where TypeOfEnemy : CryptidBehaviour
    {
        protected TypeOfEnemy cryptidBehaviour;

        public CryptidBehaviourState(TypeOfEnemy cryptidBehaviour, FSM fSM) : base()
        {
            this.cryptidBehaviour = cryptidBehaviour;
            mFsm = fSM;
        }
    }  
}