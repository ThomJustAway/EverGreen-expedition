using Assets.Scripts;
using Assets.Scripts.pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestroy<GameManager>
{
    #region fighting
    [Header("This is for experimenting, make sure to change them")]
    [SerializeField] private Turret[] startingTurret;
    [SerializeField] private Sprite playerSprite;
    public PlayerCurrentFernWeaverStats playerStats { get; private set; }
    #endregion

    #region level selection
    public int time { get; private set; }
    public int NodeIdCurrently { get; private set; }
    public bool hasSetMatrix { get; private set; }
    public int[,] adjacencyMatrix { get; private set; }
    public List<int> completedNode { get; private set; }
    #endregion

    //for fighting
    protected override void Awake()
    {
        base.Awake();
        //change this later
        InitPlayerStats();
        SetUpLevel();
    }

    private void SetUpLevel()
    {
        completedNode = new List<int>();
        completedNode.Add(0);
        NodeIdCurrently = 0;
        time = 0; //start with zero days
    }

    private void InitPlayerStats()
    {
        playerStats = new PlayerCurrentFernWeaverStats(
                50, //max leaf handle
                1000, //max hp
                0,
                0,
                1000,
                2,
                0,
                startingTurret,
                playerSprite
                );
    }

    public void TravelNode(int timeNeeded, LevelNode node)
    {
        time += timeNeeded; //add the amount of time need to travel
        int NodeTravelling = node.id;
        if(!completedNode.Contains(NodeTravelling)  //if the node not in the complete level add it in
            && node.levelDetail != Level.TeleporterLevel //if it is a teleporter just ignore
            )
        {
            completedNode.Add( NodeTravelling ); //travel to this node
        }
        
        NodeIdCurrently = NodeTravelling; //now the player is now at this node
    }

    public void UpdateStatsOnWin(int crptidRemainGain, int experienceGain)
    {
        var stats = playerStats;
        stats.cryptidRemain += crptidRemainGain;
        stats.experience += experienceGain;
        playerStats = stats;
        SceneManager.LoadScene("Level Selection");
    }

    public void UpdateAdjacenyMatrix(int[,] matrix)
    {
        hasSetMatrix = true;
        adjacencyMatrix = matrix;
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
    public int waterPerSecond;

    public int cryptidRemain;

    public PlayerCurrentFernWeaverStats(
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

