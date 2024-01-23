
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private CryptidBehaviour[] enemies;
    private Vector3 playerPosition;

    //boundaries
    private Vector3 min;
    private Vector3 max;
    private float padding = 0.5f;

    private Queue<GameObject> alertObjectPool;
    [SerializeField] private GameObject alertPrefab;
    [SerializeField]private int startingPoolNumber;

    private void Start()
    {
        alertObjectPool = new Queue<GameObject>();
        CreatePool();
        //create the pool
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
    }

    private void Update()
    {
    }
    
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

        //now with the new position just signal the 
    }

    private IEnumerator SpawningEnemyCoroutine(Vector2 position)
    {
        if(alertObjectPool.Count ==0) CreateAlert();
        if (alertObjectPool.Count > 0)
        {
            
        }

    }

    private static bool RandomBool()
    {
        return Random.value > 0.5f;
    }
}
