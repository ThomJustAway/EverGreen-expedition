using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelNode : MonoBehaviour ,IPointerDownHandler
{
    public int id {get; private set;}
    [SerializeField] private Level level; //level the thing is assign to
    public Level levelDetail { get { return level; } }

    [SerializeField] private Image image;
    [SerializeField] private Connection[] connectedNodes;

    public Connection[] ConnectedNodes { get { return connectedNodes; } }

    private void Start()
    {
        Reload();
        EventManager.Instance.AddListener(TypeOfEvent.ReloadUI, Reload);
    }

    private void Reload()
    {
        DecidedLevel();
        ChangeIcon();
    }

    private void DecidedLevel()
    {
        var completedNodes = GameManager.Instance.completedNode.ToArray();
        
        if (completedNodes.Contains(id))
        {
            if (id == GameManager.Instance.NodeIdCurrently)
            {
                level = Level.PlayerLevel;
                return;
            }
            if(level != Level.TeleporterLevel)
            {
                level = Level.CompletedLevel;
                return;
            }
        }
    }

    public void SetId(int id)
    {
        this.id = id;
    }
    //change icon of the node
    private void ChangeIcon()
    {
        switch(level)
        {
            case (Level.NormalEnemyLevel):
                image.sprite = SpriteContainer.Instance.NormalEnemyLevel;
                break;
            case (Level.TeleporterLevel):
                image.sprite = SpriteContainer.Instance.TeleporterLevel;
                break;
            case (Level.CompletedLevel):
                image.sprite = SpriteContainer.Instance.CompleteLevel;
                break;
            case (Level.PlayerLevel):
                image.sprite = SpriteContainer.Instance.PlayerLevel;
                break;
            case (Level.AddTowerLevel):
                image.sprite = SpriteContainer.Instance.AddTowerLevel;
                break;
            case(Level.BossLevel):
                image.sprite = SpriteContainer.Instance.BossLevel;
                break;
            default:
                image.sprite = SpriteContainer.Instance.CompleteLevel;
                break;
        }
    }

    //if player press the button
    public void OnPointerDown(PointerEventData eventData)
    {
        //ignore if the node is the same as the player node
        if (GameManager.Instance.NodeIdCurrently != id 
            && IsConnectedToPlayerNode() 
            || LevelSystem.Instance.isTeleporterLevel)
        {//check the adjaceny array to see if it is a node that can be click
            LevelSystem.Instance.SpawnInformationPanel(this);
        }
    }

    //if connected to player node
    public bool IsConnectedToPlayerNode()
    {
        int playerCurrentNodeID = GameManager.Instance.NodeIdCurrently;
        var mapData = GameManager.Instance.adjacencyMatrix;
        if (mapData[playerCurrentNodeID, id] != 0) return true;
        return false;
    }


}

[Serializable]
public struct Connection
{
    public LevelNode node;
    public int timeTaken;
}

public enum Level
{
    NormalEnemyLevel,
    CompletedLevel,
    PlayerLevel,
    AddTowerLevel,
    TeleporterLevel,
    BossLevel
}