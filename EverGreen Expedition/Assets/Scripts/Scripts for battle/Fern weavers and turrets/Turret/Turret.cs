using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public  class Turret : MonoBehaviour , IDamageable 
    {
        [SerializeField] protected string turretName;
        public string TurretName { get { return turretName; } }

        [SerializeField] protected int healthpoint;
        public int HealthPoint { get { return healthpoint; } }
        //cost
        [SerializeField] protected int waterCost;
        public int WaterCost { get { return waterCost; }}

        [SerializeField] protected int leafHandleCost;
        public int LeafHandleCost { get { return leafHandleCost; }}
        [SerializeField] protected Sprite turretSprite;
        public Sprite TurretSprite { get { return turretSprite; } }

        [TextArea(3 ,4 )]
        [SerializeField] private string detail;
        public string Detail { get { return detail; } }
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

        public void RemoveTurret()
        {//remove the turret
            FightingEventManager.Instance.RefundLeafHandle(leafHandleCost);
            Destroy(gameObject);
        }
    }
}