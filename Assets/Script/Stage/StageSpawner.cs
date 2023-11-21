using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    public GameObject[] StagePrefab;
    public int numOfRoom;
    public int numOfEventRoom;
    public int numberOfPortalRoom = 1;
    private StageSpawnPoint[] getSpawnPoint;
    private StageSpawnPoint[] spawnPoint;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<StageSpawnPoint>();

        numOfRoom = Random.Range(3, 5);

        if(numOfRoom < 3)
        {
            numOfRoom = Random.Range(3, 5);
        }

        numOfEventRoom = Random.Range(0, 3);
    }
    void Start()
    {
        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numOfRoom);
        
        if (getSpawnPoint.Length >= numOfRoom && StagePrefab.Length > 0)
        {
            InstantiateObjectsAtRandomPoints(numOfRoom);
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or no object prefabs to instantiate.");
        }

        StartCoroutine(DelayEventRoom(0.75f));
        StartCoroutine(DelayPortalRoom(0.8f));
    }

    void Update()
    {
        spawnPoint = GetComponentsInChildren<StageSpawnPoint>();
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
            GameObject instantiatedRoom = Instantiate(StagePrefab[0], getSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }

    private void InstantiateEventRoomAtRandomPoints(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomPrefabIndex = Random.Range(1, 2);

            GameObject instantiatedChestRoom = Instantiate(StagePrefab[randomPrefabIndex], getSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }

    private void InstantiatePortalRoomAtRandomPoints(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            //int randomPrefabIndex = Random.Range(1, StagePrefab.Length);

            GameObject instantiatedChestRoom = Instantiate(StagePrefab[3], getSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }

    IEnumerator DelayEventRoom(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numOfEventRoom);

        if (getSpawnPoint.Length >= numOfEventRoom && numOfEventRoom > 0)
        {
            InstantiateEventRoomAtRandomPoints(numOfEventRoom);
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or no object prefabs to instantiate.");
        }
    }

    IEnumerator DelayPortalRoom(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numberOfPortalRoom);

        if (getSpawnPoint.Length >= numberOfPortalRoom && numberOfPortalRoom > 0)
        {
            InstantiatePortalRoomAtRandomPoints(numberOfPortalRoom);
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or no object prefabs to instantiate.");
        }
        
    }
}

