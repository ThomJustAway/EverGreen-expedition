
using Assets.Scripts;
using Assets.Scripts.Scripts_for_level_selection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField]private CryptidBehaviour[] enemies;
    private Vector3 playerPosition;

    //boundaries
    private Vector3 min;
    private Vector3 max;
    private float padding = 0.5f;

    private Queue<GameObject> alertObjectPool;

    [Header("Alert menu")]
    [SerializeField] private GameObject alertPrefab;
    [SerializeField]private int startingPoolNumber;

    [Header("Enemies")]
    private int amountOfEnemiesToSpawn;
    private int enemiesKilled;
    private int maxAmountOfEnemy;
    private float progress;
    [SerializeField] private int numberLimitToBurst;
    [SerializeField] private int timeBeforeStartingWave;
    private void Start()
    {
        progress = 1f;
        DecideOnNumberOfEnemy();
        EventManager.Instance.CryptidDeathAddListener(CountEnemiesKilled);

        SettingUpVariables();
        UpdateUI();
        StartCoroutine(StartEnemySpawning());
    }

    private void Update()
    {
        UpdateUI();
    }

    private void DecideOnNumberOfEnemy()
    {
        int timePassed = GameManager.Instance.time;
        if ( timePassed >= (int)TimeDifficulty.Hard)
        {
            amountOfEnemiesToSpawn = 50 + 5 * ( timePassed - (int)TimeDifficulty.Hard );
        }
        else if(timePassed>= (int)TimeDifficulty.Medium) 
        {
            amountOfEnemiesToSpawn = 25 + 3 * (timePassed - (int)TimeDifficulty.Medium);
        }
        else
        {
            amountOfEnemiesToSpawn = 15 + timePassed;
        }
    }

    #region Coroutine


    //do change this to make it more complex
    private IEnumerator StartEnemySpawning()
    {
        yield return new WaitForSeconds(timeBeforeStartingWave);
        maxAmountOfEnemy = amountOfEnemiesToSpawn;
        bool doBurst = false;
        while(progress > 0f)
        {
            doBurst = DecidingIfCanBurst();

            if (doBurst)
            {
                int numberToBurst = Random.Range(2, numberLimitToBurst);
                amountOfEnemiesToSpawn -= numberToBurst;
                progress = (float)amountOfEnemiesToSpawn / (float)maxAmountOfEnemy;
                for(int i = 0; i < numberToBurst; i++)
                {
                    SpawnEnemy();
                }

                float nextWaveTiming = Random.Range(3.0f, 5.0f);
                yield return new WaitForSeconds(nextWaveTiming);

            }
            else
            {
                amountOfEnemiesToSpawn -= 1;
                progress = (float)amountOfEnemiesToSpawn / (float)maxAmountOfEnemy;
                //deciding on burst

                SpawnEnemy();

                float nextWaveTiming = Random.Range(3.0f, 5.0f);
                yield return new WaitForSeconds(nextWaveTiming);
            }
        }
    }

    private bool DecidingIfCanBurst()
    {
        bool doBurst;
        if (amountOfEnemiesToSpawn > numberLimitToBurst)
        {
            float randomNumber = Random.value;
            if (randomNumber <= 0.3)
            {
                doBurst = true;
            }
            else
            {
                doBurst = false;
            }
        }
        else
        {
            doBurst = false;
        }

        return doBurst;
    }

    private void CountEnemiesKilled(CryptidBehaviour cryptid)
    {
        enemiesKilled++;
        if(enemiesKilled == maxAmountOfEnemy)
        {
            EventManager.Instance.AlertListeners(TypeOfEvent.WinEvent);
        }
    }

    #endregion

    #region starting functions
    private void SettingUpVariables()
    {
        alertObjectPool = new Queue<GameObject>();
        CreatePool();
        //create the pool
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
    }

    private void UpdateUI()
    {
        UIController.Instance.UpdateAmountOfEnemies(amountOfEnemiesToSpawn);
        UIController.Instance.UpdateWaveProgressBar(progress);
    }

    #endregion

    #region alert creation 
    private void CreatePool()
    {
        for(int i = 0; i < startingPoolNumber; i++)
        {
            CreateAlert();
        }
    }

    private void CreateAlert()
    {
        var alert = Instantiate(alertPrefab, transform);
        alertObjectPool.Enqueue(alert);
        alert.SetActive(false);
    }
    #endregion

    #region spawn enemy
    private void SpawnEnemy()
    {
        bool fixX = RandomBool();
        Vector2 newPosition;

        if (fixX)
        { //the x coordinate
            bool minX = RandomBool();
            if (minX)
            {
                newPosition.x = min.x + padding;
            }
            else
            {
                newPosition.x = max.x - padding;
            }
            //settle the y position

            float randomYPosition = Random.Range(min.y, max.y);
            newPosition.y = randomYPosition;
        }
        else
        {
            bool minY = RandomBool();
            if (minY)
            {
                newPosition.y = min.y + padding;
            }
            else
            {
                newPosition.y = max.y - padding;
            }
            //settle the y position

            float randomXPosition = Random.Range(min.x, max.x);
            newPosition.x = randomXPosition;
        }

        //now with the new position just signal the spawning of the enemy
        StartCoroutine(SpawningEnemyCoroutine(newPosition));
    }

    private IEnumerator SpawningEnemyCoroutine(Vector2 position)
    {
        if(alertObjectPool.Count ==0) CreateAlert();
        var alert = alertObjectPool.Dequeue();

        alert.SetActive(true);
        alert.transform.localPosition = (Vector3)position;
        
        

        float spawnTiming = 10f;
        yield return new WaitForSeconds(spawnTiming);

        alert.SetActive(false);
        alertObjectPool.Enqueue(alert);//return back to the pool

        var enemy = Instantiate(enemies[0]);
        enemy.transform.position = position;
    }
    #endregion

    #region misc
    private static bool RandomBool()
    {
        return Random.value > 0.5f;
    }

    //private void OnGUI()
    //{
    //    bool spawnenemy = GUI.Button(new Rect(0, 200, 100, 100), "Spawn enemy");
    //    if (spawnenemy)
    //    {
    //        SpawnEnemy();
    //    }
    //}
    #endregion

}
