using UnityEngine;

namespace Assets.Scripts
{
    public struct LayerMaskManager
    {
        public static int turretlayerNameInt { get { return LayerMask.NameToLayer("turret"); } }
        public static int enemylayerNameInt { get {return LayerMask.NameToLayer("enemy"); } }
        public static int TurretLayerMask { get { return 1 << 3; } }
        public static int EnemyLayerMask { get { return 1 << 6; } }
    }
}