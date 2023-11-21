using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public GameObject[] propPrefabs;
    public int numOfProp;
    [SerializeField] private PropSpawnPoint[] spawnPoint;
    [SerializeField] private PropSpawnPoint[] getSpawnPoint;

    private void Awake() 
    {
        spawnPoint = GetComponentsInChildren<PropSpawnPoint>();

        numOfProp = Random.Range(5, 15);

        if(numOfProp < 3)
        {
            numOfProp = Random.Range(5, 15);
        }
    }
     void Start()
    {
        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numOfProp);
        
        if (getSpawnPoint.Length >= numOfProp && propPrefabs.Length > 0)
        {
            InstantiateObjectsAtRandomPoints(numOfProp);
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or no object prefabs to instantiate.");
        }

    }

    void Update()
    {
       
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
            int randomPrefabIndex = Random.Range(0, propPrefabs.Length);

            GameObject instantiatedRoom = Instantiate(propPrefabs[randomPrefabIndex], getSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }
}
