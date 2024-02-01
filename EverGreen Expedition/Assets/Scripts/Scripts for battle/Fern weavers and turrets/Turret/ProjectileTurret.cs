using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Fern_weavers_and_turrets
{
    public class ProjectileTurret : Turret
    {
        [Header("Projectile info")]
        [SerializeField] private int projectileDamage;
        [SerializeField] private float visibleRange;
        [SerializeField] private float attackSpeedPerSecond;
        [SerializeField] private float projectileSpeed;

        [SerializeField] private Bullet projectilePrefab;
        [SerializeField] private int initialBulletCount = 10;

        private CryptidBehaviour targetCryptid;

        private Queue<Bullet> bulletPool;

        private void Start()
        {
            bulletPool = new Queue<Bullet>();
            InitBullet();
        }

        private void Update()
        {
            TryShootNearestEnemy();
        }

        private void TryShootNearestEnemy()
        {
            if(targetCryptid == null)
            {
                var hitObject = Physics2D.CircleCast(transform.position,
                    visibleRange,
                    Vector2.zero,
                    visibleRange,
                    LayerMaskManager.EnemyLayerMask
                    );
                //try find the object

                if(hitObject.collider != null)
                {
                    targetCryptid = hitObject.transform.GetComponent<CryptidBehaviour>();
                    StartCoroutine(ShootBullet()); //continuously fire bullet until the cryptid is dead.
                }

            }
        }

        private IEnumerator ShootBullet()
        {
            while(targetCryptid != null)
            {
                Vector2 direction = targetCryptid.transform.position - transform.position;
                direction.Normalize(); //direction needed to shoot;

                if(bulletPool.Count == 0)
                {
                    CreateBullet();
                }

                Bullet bullet = bulletPool.Dequeue();
                bullet.gameObject.SetActive( true); //activate the bullet
                bullet.transform.position = transform.position; //make it appear at the turret.
                bullet.FireBullet(direction, projectileSpeed);
                yield return new WaitForSeconds(attackSpeedPerSecond); //wait for a number of second before firing the next bullet
            }



        }

        private void InitBullet()
        {
            for(int i = 0; i < initialBulletCount; i++)
            {
                CreateBullet();
            }
        }

        private void CreateBullet()
        {
            var projectile = Instantiate(projectilePrefab, transform).GetComponent<Bullet>();
            projectile.Init(projectileDamage, bulletPool);
            bulletPool.Enqueue(projectile);
            projectile.gameObject.SetActive(false);
        }
    }
}