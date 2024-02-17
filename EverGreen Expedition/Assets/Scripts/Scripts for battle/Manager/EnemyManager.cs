
using Assets.Scripts;
using Assets.Scripts.Scripts_for_level_selection;
using System.Collections;
using System.Collections.Generic;
using EventManagerYC;
using UnityEngine;
using System;

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

    private int amountOfEnemiesToSpawn;
    private int enemiesKilled;
    private int maxAmountOfEnemy;
    private float progress;
    [Header("Enemies")]
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private int numberLimitToBurst;
    [SerializeField] private int timeBeforeStartingWave;

    [Header("warning canvas")]
    [SerializeField] private WarningCanvas warningCanvas;
    private void Start()
    {
        progress = 1f;
        DecideOnNumberOfEnemy();
        EventManager.Instance.AddListener(TypeOfEvent.CryptidDeath, (Action<CryptidBehaviour>)CountEnemiesKilled);

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
        //this will calculate how many cryptid will spawn for each difficulty that increases
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
        maxAmountOfEnemy = amountOfEnemiesToSpawn;
    }

    #region Coroutine


    //do change this to make it more complex
    private IEnumerator StartEnemySpawning()
    {
        yield return new WaitForSeconds(timeBeforeStartingWave);

        warningCanvas.PlayWarning();
        maxAmountOfEnemy = amountOfEnemiesToSpawn;
        bool doBurst = false;
        while(progress > 0f)
        {
            doBurst = DecidingIfCanBurst();

            if (doBurst)
            {
                int numberToBurst = UnityEngine.Random.Range(2, numberLimitToBurst);
                amountOfEnemiesToSpawn -= numberToBurst;
                progress = (float)amountOfEnemiesToSpawn / (float)maxAmountOfEnemy;
                for(int i = 0; i < numberToBurst; i++)
                {
                    SpawnEnemy();
                }

                float nextWaveTiming = UnityEngine.Random.Range(3.0f, 5.0f);
                yield return new WaitForSeconds(nextWaveTiming);

            }
            else
            {
                amountOfEnemiesToSpawn -= 1;
                progress = (float)amountOfEnemiesToSpawn / (float)maxAmountOfEnemy;
                //deciding on burst

                SpawnEnemy();

                float nextWaveTiming = UnityEngine.Random.Range(3.0f, 5.0f);
                yield return new WaitForSeconds(nextWaveTiming);
            }
        }
    }

    private bool DecidingIfCanBurst()
    {
        bool doBurst;
        if (amountOfEnemiesToSpawn > numberLimitToBurst)
        {
            float randomNumber = UnityEngine.Random.value;
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
            EventManager.Instance.TriggerEvent(TypeOfEvent.WinEvent);
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
        UIController.Instance.UpdateAmountOfEnemies(maxAmountOfEnemy - enemiesKilled);
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

            float randomYPosition = UnityEngine.Random.Range(min.y, max.y);
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

            float randomXPosition = UnityEngine.Random.Range(min.x, max.x);
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
        SoundManager.Instance.PlayAudio(SFXClip.Alert);

        float spawnTiming = 10f;
        yield return new WaitForSeconds(spawnTiming);

        alert.SetActive(false);
        alertObjectPool.Enqueue(alert);//return back to the pool

        var enemySelected = enemies[UnityEngine.Random.Range(0,enemies.Length)];

        var enemy = Instantiate(enemySelected , enemyContainer);
        enemy.transform.localPosition= position;
    }
    #endregion

    #region misc
    private static bool RandomBool()
    {
        return UnityEngine.Random.value > 0.5f;
    }

    #endregion

}
