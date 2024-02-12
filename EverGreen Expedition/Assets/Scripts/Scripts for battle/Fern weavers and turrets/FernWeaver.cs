using Assets.Scripts;
using EventManagerYC;
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

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerStats.fernWeaverSprite;
    }

    public void TakeDamage(int amountOfDamage)
    {
        //show the damage
        EventManager.Instance.TriggerEvent(TypeOfEvent.ShowDamagePopUp, (Vector2)transform.position, amountOfDamage);
        
        if (!isInvincible)
        {
            FightingEventManager.Instance.TakeDamage(amountOfDamage);
        }
    }
}
