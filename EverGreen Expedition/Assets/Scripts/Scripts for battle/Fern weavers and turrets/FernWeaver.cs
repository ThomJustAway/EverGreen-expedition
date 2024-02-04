using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernWeaver : MonoBehaviour , IDamageable
{
    [SerializeField] private bool isInvincible;
    private void Awake()
    {
        gameObject.layer = LayerMaskManager.turretlayerNameInt;
    }

    public void TakeDamage(int amountOfDamage)
    {
        if(!isInvincible)
        {
            FightingEventManager.Instance.TakeDamage(amountOfDamage);
        }
    }
}
