using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FernWeaver : MonoBehaviour , IDamageable
{
    private void Awake()
    {
        gameObject.layer = LayerMaskManager.turretlayerNameInt;
    }

    public void TakeDamage(int amountOfDamage)
    {
        FightingEventManager.Instance.TakeDamage(amountOfDamage);
    }
}
