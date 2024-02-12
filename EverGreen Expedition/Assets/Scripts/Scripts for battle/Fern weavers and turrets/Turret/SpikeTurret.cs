using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeTurret : Turret
{
    [SerializeField] private int damage;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float hitRange;

    private bool hasTarget = false;
    private void Update()
    {
        if (!hasTarget)
        {
            TryHitCryptidsInRange();
        }
        //else just let the coroutine handle the work
    }

    private void TryHitCryptidsInRange()
    {
        var hitObjects = Physics2D.CircleCastAll(transform.position,
                  hitRange,
                  Vector2.zero,
                  hitRange,
                  LayerMaskManager.EnemyLayerMask
                  );
        if( hitObjects.Length > 0 )
        {//
            hasTarget = true;
            StartCoroutine(StartHittingTargets());
        }
    }

    private IEnumerator StartHittingTargets()
    {
        while (true)
        {
            var hitObjects = Physics2D.CircleCastAll(transform.position,
                      hitRange,
                      Vector2.zero,
                      hitRange,
                      LayerMaskManager.EnemyLayerMask
                      );
            if(hitObjects.Length == 0 ) 
            {
                hasTarget = false; //stop having the target and wait for the next cryptid to come
                break; //stop the infinite loop
            }
            else
            {
                foreach(var hitObject in hitObjects)
                {
                    hitObject.transform.GetComponent<IDamageable>().TakeDamage(damage);
                } //damage all the cryptid nearby

                //then wait if there is any more cryptids
                yield return new WaitForSeconds(rechargeTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }

}
