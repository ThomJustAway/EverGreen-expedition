using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Fern_weavers_and_turrets
{
    public class Bullet : MonoBehaviour
    {
        private int projectileDamage;
        private Queue<Bullet> referencePool;
        [SerializeField] private float radiusOfBullet;
        [SerializeField] private MusicClip bulletHitClip;
        private SpriteRenderer render;

        public void Init(int damage , Queue<Bullet> pool)
        {
            projectileDamage = damage;
            referencePool = pool;

            render = gameObject.GetComponent<SpriteRenderer>();
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;

            audioSource.clip = bulletHitClip.clip;
            audioSource.pitch = bulletHitClip.pitch;
            audioSource.volume = bulletHitClip.volume;
            bulletHitClip.source = audioSource;
        }

        public void FireBullet(Vector2 direction , float speed)
        {
            render.enabled = true;
            StartCoroutine(MoveBullet(direction, speed));
        }

        
        private IEnumerator MoveBullet(Vector2 direction, float speed)
        {
            float elapseTime = 0f;

            while (elapseTime < 10f)
            {
                transform.Translate(direction * speed * Time.deltaTime); //move the bullet
                var hitObject = Physics2D.CircleCast(
                    transform.position,
                   radiusOfBullet,
                   Vector2.zero,
                   radiusOfBullet,
                   LayerMaskManager.EnemyLayerMask
                   );

                if(hitObject.collider != null)
                {//is an enemy and the bullet hit\
                    bulletHitClip.source.Play();
                    IDamageable componentToDamage = hitObject.transform.GetComponent<IDamageable>();
                    componentToDamage.TakeDamage(projectileDamage); 
                    break;
                }
                //if nothing
                elapseTime += Time.deltaTime;
                yield return null;
            }

            ReturnBullet(); 
        }

        private void ReturnBullet()
        {
            render.enabled = false;
            referencePool.Enqueue(this); //let the turret know that the bullet is ready to fire again.
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.yellow;  
        //    Gizmos.DrawWireSphere(transform.position, radiusOfBullet);  
        //}
    }
}