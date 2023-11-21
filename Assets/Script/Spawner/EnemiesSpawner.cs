using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    StageManager stageManager;

    [Header("Enemies Manage")]
    public GameObject wall;
    [SerializeField] private Enemy[] enemies;
    public bool foundPlayer;
    public float radius;
    public int enemiesCount;
    public LayerMask targetLayer;
    public LayerMask targetPlayerLayer;


    [Header("Spawner")]
    public GameObject[] enemiesPrefab;
    public int numOfEnemies;
    [SerializeField] private EnemiesSpawnPoint[] spawnPoint;
    [SerializeField] private EnemiesSpawnPoint[] getSpawnPoint;

    private void Awake() 
    {
        spawnPoint = GetComponentsInChildren<EnemiesSpawnPoint>();

        stageManager = GetComponentInParent<StageManager>();

        numOfEnemies = Random.Range(3, 10);

        if(numOfEnemies < 3)
        {
            numOfEnemies = Random.Range(3, 10);
        }
    }
     void Start()
    {
        wall.SetActive(false);

        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numOfEnemies);
        
        if (getSpawnPoint.Length >= numOfEnemies && enemiesPrefab.Length > 0 && !stageManager.baseStage)
        {
            InstantiateObjectsAtRandomPoints(numOfEnemies);
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or no object prefabs to instantiate.");
        }

    }

    void Update()
    {
        if(enemiesCount <= 0) wall.SetActive(false);

        FindPlayer();
        FindEnemies();
    }

    public void FindEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        enemiesCount = colliders.Length;

        foreach (Collider2D enemy in colliders)
        {
            enemies = enemy.GetComponents<Enemy>();            

            if(foundPlayer)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].isActive = true;
                }
            }
        }
    }

    public void FindPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetPlayerLayer);

        foreach (Collider2D player in colliders)
        {
            foundPlayer = true;

            if(enemiesCount > 0) wall.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private T[] GetRandomSubsetArray<T>(T[] sourceArray, int subsetSize)
    {
        if (subsetSize >= sourceArray.Length)
            return sourceArray;

        T[] randomSubset = new T[subsetSize];
        T[] remainingItems = (T[])sourceArray.Clone();

        for (int i = 0; i < subsetSize; i++)
        {
            int randomIndex = Random.Range(0, remainingItems.Length);
            randomSubset[i] = remainingItems[randomIndex];

            // Remove the selected item from the remaining items.
            remainingItems[randomIndex] = remainingItems[remainingItems.Length - 1];
            System.Array.Resize(ref remainingItems, remainingItems.Length - 1);
        }

        return randomSubset;
    }

    private void InstantiateObjectsAtRandomPoints(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomPrefabIndex = Random.Range(0, enemiesPrefab.Length);

            GameObject instantiatedRoom = Instantiate(enemiesPrefab[randomPrefabIndex], getSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }

}
