using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FernWeaver : MonoBehaviour , IDamageable
{
    //leaf handle
    [SerializeField] private int maxLeafHandle;
    public int currentLeafHandle { get; private set; }

    //water
    [SerializeField] public int waterResources { get; private set; }
    //hp
    [SerializeField] private int maxHP;
    [SerializeField] public int currentHP { get; private set; }

    [SerializeField] protected int level = 0;
    [SerializeField] protected int experience;
    [SerializeField] protected int experienceNeededForNextLevel; 
    
    public void StartNewGame()
    {
        //reset the values
        currentHP = maxHP;
        currentLeafHandle = maxLeafHandle;

        //this can be changed if you want.
        waterResources = 100;
    }

    private void Start()
    {
        StartNewGame();
    }

    private void Update()
    {
        UIController.Instance.UpdateUI(maxHP, currentHP, maxLeafHandle ,currentLeafHandle , waterResources);
    }

    public void TakeDamage(int amountOfDamage)
    {
        currentHP -= amountOfDamage;
        if(currentHP < 0)
        {
            currentHP = 0;
        }
    }

    private void OnGUI()
    {
        bool isButtonClicked = GUI.Button(new Rect(0, 0, 100, 100) , "take damage");
        if(isButtonClicked)
        {
            TakeDamage(100);
        }
    }
}
