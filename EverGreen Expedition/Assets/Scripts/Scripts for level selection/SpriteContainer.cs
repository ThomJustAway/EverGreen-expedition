using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContainer : Singleton<SpriteContainer>   
{
    [SerializeField] private Sprite normalEnemyLevel;
    [SerializeField] private Sprite playerLevel;
    [SerializeField] private Sprite completeLevel;
    [SerializeField] private Sprite addTowerLevel;
    [SerializeField] private Sprite teleporterLevel;
    [SerializeField] private Sprite bossLevel;

    public Sprite NormalEnemyLevel { get { return normalEnemyLevel; } }
    public Sprite PlayerLevel { get { return playerLevel; } }
    public Sprite CompleteLevel { get { return completeLevel; } }
    public Sprite AddTowerLevel { get { return addTowerLevel; } }
    public Sprite TeleporterLevel { get { return teleporterLevel;} }
    public Sprite BossLevel { get { return bossLevel; } }

}
