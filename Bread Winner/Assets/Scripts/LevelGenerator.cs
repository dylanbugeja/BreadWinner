using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float SPAWN_DISTANCE = 250f;

    [SerializeField] private Transform levelStart;
    [SerializeField] private List<Transform> levelPartList;
    //Just going to reference player 1 for now needs to be fixed later
    public List<GameObject> Players = new List<GameObject>();

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelStart.Find("EndPosition").position;

        int startingSpawnParts = 5;

        for (int i = 0; i < startingSpawnParts; i++)
        {
            SpawnLevelPart();
        }
    }
    private void Update()
    {
        foreach (GameObject p in Players)
        {
            if (Vector3.Distance(p.transform.position, lastEndPosition) < SPAWN_DISTANCE)
            {
                SpawnLevelPart();
            }
        }
    }
    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);

        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}

