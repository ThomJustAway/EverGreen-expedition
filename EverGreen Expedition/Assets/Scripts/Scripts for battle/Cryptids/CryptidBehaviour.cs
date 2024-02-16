using Assets.Scripts;
using Assets.Scripts.EnemyFSM;
using PGGE.Patterns;

using System.Collections;
using EventManagerYC;
using UnityEngine;

public class CryptidBehaviour : MonoBehaviour , IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;

    [Header("Reward")]
    [SerializeField] private int minExp = 10;
    [SerializeField] private int maxExp = 20;

    [SerializeField] private int cryptidRemainMin = 10;
    [SerializeField] private int cryptidRemainMax = 10;

    public int MinExp { get { return minExp; } }
    public int MaxExp { get { return maxExp; } }
    public int CryptidRemainMin { get { return cryptidRemainMin; } }
    public int CryptidRemainMax { get { return cryptidRemainMax; } }


    public int Damage { get { return damage; } }
    [Header("Attacks")]

    [SerializeField] private float attackSpeedPerSecond = 1;
    public float AttackSpeedPerSecond { get { return attackSpeedPerSecond; } }

    [SerializeField] private float attackRadius;
    public float AttackRadius { get { return attackRadius; } }
    [SerializeField] private float movementSpeed = 5;
    public float MovementSpeed { get { return movementSpeed; } }
    private FSM fsm;

    #region misc
    private Collider2D cryptidCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private MusicClip chewingMusicClip;
    [SerializeField] private MusicClip deathMusicClip;
    #endregion

    private void Awake()
    {
        //set up the FSM
        SettingUpFSM();

        //make sure the cryptid is under the enemylayer mask so that it can be hit by the bullet
        gameObject.layer = LayerMaskManager.enemylayerNameInt;
    }

    protected virtual void SettingUpFSM()
    {
        fsm = new FSM();
        fsm.Add((int)EnemyState.move, new MovingState(fsm, this));
        fsm.Add((int)EnemyState.attack, new AttackingState(this, fsm));
        fsm.SetCurrentState((int)EnemyState.move);
    }

    private void Start()
    {
        InitOnStart();
    }

    protected virtual void InitOnStart()
    {
        cryptidCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SettingUpMusic();
    }

    private void SettingUpMusic()
    {
        var chewingSoundEffect = gameObject.AddComponent<AudioSource>();
        chewingSoundEffect.volume = chewingMusicClip.volume;
        chewingSoundEffect.pitch = chewingMusicClip.pitch;
        chewingSoundEffect.clip = chewingMusicClip.clip;
        chewingMusicClip.source = chewingSoundEffect;

        var deathSoundEffect = gameObject.AddComponent<AudioSource>();
        deathSoundEffect.volume = deathMusicClip.volume;
        deathSoundEffect.pitch = deathMusicClip.pitch;
        deathSoundEffect.clip = deathMusicClip.clip;
        deathMusicClip.source = deathSoundEffect;
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
        EventManager.Instance.TriggerEvent(TypeOfEvent.ShowDamagePopUp, (Vector2)transform.position, amountOfDamage);
        if(health < 0)
        {
            health = 0;
            EventManager.Instance.TriggerEvent(TypeOfEvent.CryptidDeath,this);
            StartCoroutine(DeathCoroutine());
        }
    }

    public void PlayAttackSoundEffect()
    {
        chewingMusicClip.source.Play();
    }

    //start its death animation
    private IEnumerator DeathCoroutine()
    {
        deathMusicClip.source.Play();
        spriteRenderer.enabled = false;
        cryptidCollider.enabled = false;
        yield return new WaitForSeconds(1.4f); //wait for the death animation ends
        Destroy(gameObject);
    }
}

public enum EnemyState
{
    move,
    attack
}