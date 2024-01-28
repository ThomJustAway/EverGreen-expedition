using Assets.Scripts;
using Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : Singleton<LevelSystem>
{
    //make an adjaceny matrix to contain the values
    [SerializeField] private LevelNode[] nodes; //wil sort it based on the level
    private int[,] adjacencyMatrix;
    //so how does the adjaceny matrix work?
    // [will contain the number in time,]

    [SerializeField] private RectTransform canvasRectTransformForEnvironment;

    #region information panel related
    [SerializeField] private RectTransform InformationPanel;
    [SerializeField] private Button Travelbutton;
    [SerializeField] private TextMeshProUGUI levelTextName;
    [SerializeField] private TextMeshProUGUI timeText;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        SetUpId();
    }

    private void Start()
    {
        if (!GameManager.Instance.hasSetMatrix)
        {
            CreateAdjacenyMatrix();
        }
        
    }

    private void SetUpId()
    {
        for(int i  = 0; i < nodes.Length; i++)
        {
            var node = nodes[i];
            node.SetId(i);
        }
    }

    private void CreateAdjacenyMatrix()
    {
        adjacencyMatrix = new int[nodes.Length, nodes.Length];
        for(int i = 0;i < nodes.Length;i++)
        {
            var node = nodes[i];
            foreach(var connection in node.ConnectedNodes)
            {
                int idOfNode = connection.node.id;
                adjacencyMatrix[i, idOfNode] = connection.timeTaken;
            }
        }
        GameManager.Instance.UpdateAdjacenyMatrix(adjacencyMatrix);
        //PrintMatrix();
    }

    private void PrintMatrix()
    {
        string row = "";
        for(int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
            {
                row += adjacencyMatrix[i, j] + " ";
            }
            row += "\n";
        }
        print(row);    
    }

    public void SpawnInformationPanel(LevelNode node)
    {
        //will try to put it at the left
        InformationPanel.gameObject.SetActive(true);
        //calculate the total length of the panel
        var widthOfPanel = InformationPanel.rect.width * InformationPanel.localScale.x ;

        //then get the same thing for the node and get the length of the width
        var nodeRect = node.transform.GetComponent<RectTransform>();
        var widthOfNode = nodeRect.rect.width * nodeRect.localScale.x ;


        //find if the the information panel can fit through the entire screen
        float widthOfTheWholeThing = nodeRect.anchoredPosition.x + widthOfPanel + widthOfNode / 2;
        
        //if it can fit the entire right side of the screen, then find the anchor position of the rect transform
        if (widthOfTheWholeThing > canvasRectTransformForEnvironment.rect.width/2)
        {//cant do it at the left
            Vector2 newPosition = nodeRect.anchoredPosition;
            newPosition.x -= widthOfNode / 2 + widthOfPanel / 2;
            InformationPanel.anchoredPosition = newPosition;
        }
        else
        {//reverse and place the rect transform on the left.
            Vector2 newPosition = nodeRect.anchoredPosition;
            newPosition.x += widthOfNode/2 + widthOfPanel/2;
            InformationPanel.anchoredPosition = newPosition;
        }


        //set up the informationPanel
        int timeNeeded = adjacencyMatrix[GameManager.Instance.NodeIdCurrently, node.id];
        timeText.text = $"Travel Time: <color=blue>{timeNeeded} Day";
        levelTextName.text = NameBasedOnLevel(node.levelDetail);

        Travelbutton.onClick.RemoveAllListeners();
        Travelbutton.onClick.AddListener(() => { TravelToNode(node, timeNeeded); });
    }

    private string NameBasedOnLevel(Level level)
    {
        switch (level)
        {
            case (Level.NormalEnemyLevel):
                return "Normal Enemy";
            case (Level.AddTowerLevel):
                return "Add DNA";
            case (Level.CompletedLevel):
                return "Completed Level";
            case (Level.TeleporterLevel):
                return "Teleporter";
            default:
                return "Unknown";
        }
    }

    private void TravelToNode(LevelNode node , int time)
    {
        //inform the game manager that the player is moving
        GameManager.Instance.TravelNode(time, node.id);

        switch (node.levelDetail)
        {
            case (Level.NormalEnemyLevel):
                TravelNormalEnemyLevel();
                break;
            case (Level.TeleporterLevel):
                TravelTeleporterLevel();
                break;
            case (Level.AddTowerLevel):
                TravelAddPlantLevel();
                break;
            case (Level.BossLevel):
                TravelBossLevel();
                break;
            case (Level.CompletedLevel):
                TravelCompletedLevel();
                break;
            default:
                Debug.LogError("Node cant be traveled");
                break;
        }
    }

    #region travel node function
    
    private void TravelNormalEnemyLevel()
    {
        SceneManager.LoadScene("Battle scene");

    }

    private void TravelCompletedLevel()
    {
        EventManager.Instance.AlertListeners(TypeOfEvent.ReloadUI);
    }

    private void TravelAddPlantLevel()
    {
        //Show add plant level
    }

    private void TravelTeleporterLevel()
    {
        //go to travel teleporter
    }

    private void TravelBossLevel()
    {
        //go to travel to boss level
    }

    #endregion

}


