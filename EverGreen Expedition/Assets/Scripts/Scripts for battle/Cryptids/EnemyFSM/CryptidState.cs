using PGGE.Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyFSM
{
    public class CryptidState : FSMState
    {
        protected CryptidBehaviour cryptidBehaviour;

        public CryptidState(CryptidBehaviour cryptidBehaviour , FSM fSM): base()
        {
            this.cryptidBehaviour = cryptidBehaviour;
            mFsm = fSM;
        }


        
    }
}