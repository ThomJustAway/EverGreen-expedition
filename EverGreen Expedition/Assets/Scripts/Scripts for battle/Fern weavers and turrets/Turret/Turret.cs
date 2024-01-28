using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public  class Turret : MonoBehaviour , IDamageable
    {
        [SerializeField] protected string turretName;
        public string TurretName { get { return turretName; } }

        [SerializeField] protected int healthpoint;
        //cost
        [SerializeField] protected int waterCost;
        public int WaterCost { get { return waterCost; }}
        [SerializeField] protected int leafHandleCost;
        public int LeafHandleCost { get { return leafHandleCost; }}
        [SerializeField] protected Sprite turretSprite;
        public Sprite TurretSprite { get { return turretSprite; } }
        protected virtual void Awake()
        {
            gameObject.layer = LayerMaskManager.turretlayerNameInt;
        }

        public virtual void TakeDamage(int amountOfDamage)
        {
            healthpoint -= amountOfDamage;
            if(healthpoint <= 0 ) { 
                healthpoint = 0; 
                FightingEventManager.Instance.RefundLeafHandle(leafHandleCost);
                Destroy(gameObject); 
            }
        }
    }
}