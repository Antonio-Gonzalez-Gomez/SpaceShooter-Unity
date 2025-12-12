using System;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    [SerializeField] Spawner[] enemySpawners;
    [SerializeField] SpawnerPowerUp powerupSpawner;
    [SerializeField] int totalEnemyCost = 5;
    [SerializeField] int costIncrement = 1;
    [SerializeField] float waveTimer = 5f;
    [SerializeField] int powerUpFrecuency = 3;

    public Dictionary<SpawnLocation, int> indexDict;
    public enum SpawnLocation
    {
        Top,
        Bottom,
        Right
    }

    private Vector3[] spawnLines;
    private int[] enemyCosts;
    private int spawnersCount;

    void Start()
    {
        spawnersCount = enemySpawners.Length;
        spawnLines = new Vector3[6];
        enemyCosts = new int[spawnersCount];

        indexDict = new Dictionary<SpawnLocation, int>();
        indexDict.Add(SpawnLocation.Top, 0);
        indexDict.Add(SpawnLocation.Bottom, 2);
        indexDict.Add(SpawnLocation.Right, 4);
        InitBordersAndSpawnLines();

        for (int i = 0; i < spawnersCount; i++)
        {
            enemyCosts[i] = enemySpawners[i].enemyCost;
        }

        InvokeRepeating(nameof(ExecuteSpawning), 0f, waveTimer);
    }

    void ExecuteSpawning()
    {
        int[] enemyCounts = GenerateEnemyQuantities();
        for (int i = 0; i < spawnersCount; i++)
        {
            if (enemyCounts[i] == 0)
                continue;
            Spawner spawner = enemySpawners[i];
            int lineIndex = indexDict.GetValueOrDefault(spawner.location);
            StartCoroutine(spawner.SpawnEnemies(enemyCounts[i], waveTimer, spawnLines[lineIndex], spawnLines[lineIndex + 1]));
        }
        totalEnemyCost += costIncrement;
        if (totalEnemyCost % powerUpFrecuency == 0)
            powerupSpawner.SpawnPowerUp(spawnLines[4], spawnLines[5]);
    }

    private int[] GenerateEnemyQuantities()
    {
        int[] result = new int[spawnersCount];
        Array.Clear(result, 0, spawnersCount);
        int remainingCost = totalEnemyCost;
        while (remainingCost > 0)
        {
            int index = UnityEngine.Random.Range(0, spawnersCount);
            result[index]++;
            remainingCost -= enemyCosts[index];
        }
        return result;
    }

    void InitBordersAndSpawnLines()
    {
        Transform topBorder = new GameObject().transform;
        Transform bottomBorder = new GameObject().transform;
        Transform leftBorder = new GameObject().transform;
        Transform rightBorder = new GameObject().transform;

        topBorder.gameObject.AddComponent<BoxCollider2D>();
        bottomBorder.gameObject.AddComponent<BoxCollider2D>();
        leftBorder.gameObject.AddComponent<BoxCollider2D>();
        rightBorder.gameObject.AddComponent<BoxCollider2D>();

        topBorder.name = "TopBorder";
        bottomBorder.name = "BottomBorder";
        leftBorder.name = "LeftBorder";
        rightBorder.name = "RightBorder";

        topBorder.tag = "ScreenBorder";
        bottomBorder.tag = "ScreenBorder";
        leftBorder.tag = "ScreenBorder";
        rightBorder.tag = "ScreenBorder";

        Vector2 screenSize;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        Vector3 cameraPos = Camera.main.transform.position;

        float borderWidth = 1f;

        topBorder.localScale = new Vector3(screenSize.x * 2, borderWidth, borderWidth);
        bottomBorder.localScale = new Vector3(screenSize.x * 2, borderWidth, borderWidth);
        leftBorder.localScale = new Vector3(borderWidth, screenSize.y * 2, borderWidth);
        rightBorder.localScale = new Vector3(borderWidth, screenSize.y * 2, borderWidth);

        topBorder.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topBorder.localScale.y * 0.5f), 0);
        bottomBorder.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomBorder.localScale.y * 0.5f), 0);
        leftBorder.position = new Vector3(cameraPos.x - screenSize.x - (leftBorder.localScale.x * 0.5f), cameraPos.y, 0);
        rightBorder.position = new Vector3(cameraPos.x + screenSize.x + (rightBorder.localScale.x * 0.5f), cameraPos.y, 0);

        spawnLines[0] = new Vector3(topBorder.position.x + 1f / 5 * screenSize.x, topBorder.position.y, 0);
        spawnLines[1] = new Vector3(topBorder.position.x + 2f / 5 * screenSize.x, topBorder.position.y, 0);
        spawnLines[2] = new Vector3(bottomBorder.position.x + 1f / 5 * screenSize.x, bottomBorder.position.y, 0);
        spawnLines[3] = new Vector3(bottomBorder.position.x + 2f / 5 * screenSize.x, bottomBorder.position.y, 0);
        spawnLines[4] = new Vector3(rightBorder.position.x, rightBorder.position.y + 2f / 5 * screenSize.y, 0);
        spawnLines[5] = new Vector3(rightBorder.position.x, rightBorder.position.y - 2f / 5 * screenSize.y, 0);
    }

}
