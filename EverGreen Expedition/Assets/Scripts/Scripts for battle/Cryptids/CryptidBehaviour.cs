using Assets.Scripts;
using Assets.Scripts.EnemyFSM;
using PGGE.Patterns;

using System.Collections;
using EventManagerYC;
using UnityEngine;

public class CryptidBehaviour : MonoBehaviour , IDamageable
{
    [SerializeField] protected int health = 100;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float movementSpeed = 5;
    public float MovementSpeed { get { return movementSpeed; } }

    [Header("Reward")]
    [SerializeField] protected int minExp = 10;
    [SerializeField] protected int maxExp = 20;

    [SerializeField] protected int cryptidRemainMin = 10;
    [SerializeField] protected int cryptidRemainMax = 10;

    public int MinExp { get { return minExp; } }
    public int MaxExp { get { return maxExp; } }
    public int CryptidRemainMin { get { return cryptidRemainMin; } }
    public int CryptidRemainMax { get { return cryptidRemainMax; } }


    public int Damage { get { return damage; } }
    [Header("Attacks")]

    [SerializeField] protected float attackSpeedPerSecond = 1;
    public float AttackSpeedPerSecond { get { return attackSpeedPerSecond; } }

    [SerializeField] protected float attackRadius;
    public float AttackRadius { get { return attackRadius; } }
    protected FSM fsm;

    #region misc
    protected Collider2D cryptidCollider;
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected MusicClip chewingMusicClip;
    [SerializeField] protected MusicClip deathMusicClip;
    #endregion

    protected virtual void Awake()
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

    public virtual void TakeDamage(int amountOfDamage)
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
    protected virtual IEnumerator DeathCoroutine()
    {
        deathMusicClip.source.Play();
        spriteRenderer.enabled = false;
        cryptidCollider.enabled = false;
        yield return new WaitForSeconds(1.4f); //wait for the death animation ends
        Destroy(gameObject);
    }
}
