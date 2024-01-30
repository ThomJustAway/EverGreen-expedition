using Assets.Scripts;
using System;
using UnityEngine;

[Serializable]
public struct PlayerCurrentFernWeaverStats
{
    public string name;

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
    public int waterPerSecond;
    public int cryptidRemain;

    public PlayerCurrentFernWeaverStats(
        string name,
        int maxLeafHandle,
        int maxHp,
        int level,
        int experience,
        int experienceNeededForNextLevel,
        int waterPerSecond,
        int cryptidRemain,
        Turret[] turrets,
        Sprite fernWeaverSprite
        )
    {
        this.name = name;
        this.maxLeafHandle = maxLeafHandle;
        maxHP = maxHp;
        this.level = level;
        this.experience = experience;
        this.experienceNeededForNextLevel = experienceNeededForNextLevel;
        this.turrets = turrets; 
        this.fernWeaverSprite = fernWeaverSprite;
        this.waterPerSecond = waterPerSecond;
        this.cryptidRemain = cryptidRemain;
    }

}

