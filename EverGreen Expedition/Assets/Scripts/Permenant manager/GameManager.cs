using Assets.Scripts;
using Assets.Scripts.pattern;
using EventManagerYC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestroy<GameManager>
{
    [Header("test only")]
    [SerializeField] private bool testing;
    [SerializeField] private PlayerCurrentFernWeaverStats testingStats;

    public PlayerCurrentFernWeaverStats playerStats; /*{ get; private set; }*/
    #region level selection
    public int time { get; private set; }
    public int NodeIdCurrently { get; private set; }
    public bool hasSetMatrix { get; private set; }
    public int[,] adjacencyMatrix { get; private set; }
    public List<int> completedNode { get; private set; }
    #endregion'

    #region tutorial
    public bool HasStartedTutorial { get; private set; } = false;
    public bool HasStartedBattleTutorial { get; private set; } = false;
    #endregion
    //for fighting
    protected override void Awake()
    {
        base.Awake();
        //change this later
        SetUpLevel();
        if (testing)
        {
            playerStats = testingStats;
        }
    }



    private void SetUpLevel()
    {
        completedNode = new List<int>();
        MoveToStartingPosition();
        time = 0; //start with zero days
    }

    private void MoveToStartingPosition()
    {
        completedNode.Add(0);
        NodeIdCurrently = 0;
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

    public void AddTimeForDebug()
    {
        time += 10;
    }

    //when player click the continue button after they win.
    public void UpdateStatsOnWin(int crptidRemainGain, int experienceGain)
    {
        Time.timeScale = 1f; //make sure the time resumes
        var stats = playerStats;
        stats.cryptidRemain += crptidRemainGain;
        stats.experience += experienceGain;
        playerStats = stats;
        EventManager.Instance.ResetManager();
        SceneManager.LoadScene("Level Selection");
    }

    public void UpdateAdjacenyMatrix(int[,] matrix)
    {
        hasSetMatrix = true;
        adjacencyMatrix = matrix;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SetUpLevel();
    }

    public void SetPlayerState(PlayerCurrentFernWeaverStats stats)
    {
        playerStats = stats;
    }

    public void HasPlayedTutorial()
    {
        HasStartedTutorial = true;
    }

    public void HasPlayedBattleTutorial()
    {
        HasStartedBattleTutorial = true;
    }
}

