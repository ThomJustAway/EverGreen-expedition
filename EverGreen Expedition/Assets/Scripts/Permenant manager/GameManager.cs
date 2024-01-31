using Assets.Scripts;
using Assets.Scripts.pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestroy<GameManager>
{

    public PlayerCurrentFernWeaverStats playerStats { get; private set; }

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
        SetUpLevel();
    }

    private void SetUpLevel()
    {
        completedNode = new List<int>();
        completedNode.Add(0);
        NodeIdCurrently = 0;
        time = 0; //start with zero days
    }

    public void AddTurret(Turret turret)
    {
        playerStats.turrets.Add(turret);
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

    public void SetPlayerState(PlayerCurrentFernWeaverStats stats)
    {
        playerStats = stats;
    }
}

