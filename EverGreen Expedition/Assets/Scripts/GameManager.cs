using Assets.Scripts;
using Assets.Scripts.pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    [Header("This is for experimenting, make sure to change them")]
    [SerializeField] private Turret[] startingTurret;
    [SerializeField] private Sprite playerSprite;
    public PlayerCurrentFernWeaverStats playerStats { get; private set; }

    //for fighting
    private void Awake()
    {
        playerStats = new PlayerCurrentFernWeaverStats(
        50, //max leaf handle
        1000, //max hp
        0,
        0,
        1000,
        startingTurret,
        playerSprite
        );
    }
}

public struct PlayerCurrentFernWeaverStats
{
    //leaf handle
    public int maxLeafHandle;

    //max Health
    public int maxHP;

    //player current level
    public int level;
    public int experience;
    public int experienceNeededForNextLevel;
    //turrets the player has
    public Turret[] turrets;

    public Sprite fernWeaverSprite;
    public PlayerCurrentFernWeaverStats(
        int maxLeafHandle,
        int maxHp,
        int level,
        int experience,
        int experienceNeededForNextLevel,
        Turret[] turrets,
        Sprite fernWeaverSprite
        )
    {
        this.maxLeafHandle = maxLeafHandle;
        maxHP = maxHp;
        this.level = level;
        this.experience = experience;
        this.experienceNeededForNextLevel = experienceNeededForNextLevel;
        this.turrets = turrets; 
        this.fernWeaverSprite = fernWeaverSprite;
    }

}

