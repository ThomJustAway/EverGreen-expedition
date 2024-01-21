using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public  class Turret : MonoBehaviour , IDamageable
    {
        [SerializeField] protected string turretName;
        public string Name { get { return turretName; } }

        [SerializeField] protected int healthpoint;



        //cost
        [SerializeField] protected int waterCost;
        [SerializeField] protected int leafHandleCost;

        protected virtual void Awake()
        {
            gameObject.layer = LayerMaskManager.turretlayerNameInt;
        }

        public virtual void TakeDamage(int amountOfDamage)
        {
            healthpoint -= amountOfDamage;
            if(healthpoint <= 0 ) { healthpoint = 0; }
            //do change this
            Destroy(gameObject); 
        }
    }
}