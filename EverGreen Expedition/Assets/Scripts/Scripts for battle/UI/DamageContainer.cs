using Assets.Scripts.pattern;
using Assets.Scripts.UI_related.Misc;
using EventManagerYC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContainer : MonoBehaviour
{
    private PoolingPattern<DamagePopUp> poolOfDamage;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int startingPooling;

    private void Start()
    {
        poolOfDamage = new PoolingPattern<DamagePopUp>(prefab);
        poolOfDamage.InitWithParent(startingPooling, transform);

        EventManager.Instance.AddListener(TypeOfEvent.ShowDamagePopUp,(Action<Vector2, int>)InitDamagePopUp);
    }
    
    public void InitDamagePopUp(Vector2 position , int damage)
    {
        var damagePopUp = poolOfDamage.Get();
        damagePopUp.Init(this);
        damagePopUp.transform.position = position;
        damagePopUp.Play(damage);
    }

    public void Retrieve(DamagePopUp damagePopUp)
    {
        poolOfDamage.Retrieve(damagePopUp);
    }

}
