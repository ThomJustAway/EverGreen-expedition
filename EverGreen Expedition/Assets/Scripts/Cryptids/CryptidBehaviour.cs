using Assets.Scripts;
using Assets.Scripts.EnemyFSM;
using PGGE.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptidBehaviour : MonoBehaviour , IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    public int Damage { get { return damage; } }

    [SerializeField] private float attackSpeedPerSecond = 1;
    public float AttackSpeedPerSecond { get { return attackSpeedPerSecond; } }

    [SerializeField] private float attackRadius;
    public float AttackRadius { get { return attackRadius; } }
    [SerializeField] private float movementSpeed = 5;
    public float MovementSpeed { get { return movementSpeed; } }

    private FSM fsm;

    private void Awake()
    {
        fsm = new FSM();
        fsm.Add((int) EnemyState.move , new MovingState(fsm, this));
        fsm.Add((int) EnemyState.attack , new AttackingState(this , fsm));
        fsm.SetCurrentState((int)EnemyState.move );
        gameObject.layer = LayerMaskManager.enemylayerNameInt;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Update()
    {
        fsm.Update();
    }

    public void TakeDamage(int amountOfDamage)
    {
        health -= amountOfDamage;
        if(health < 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
}

public enum EnemyState
{
    move,
    attack
}