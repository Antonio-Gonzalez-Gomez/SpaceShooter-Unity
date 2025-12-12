using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] public int enemyCost;
    [SerializeField] public MasterSpawner.SpawnLocation location;

    private Vector3 defaultDirection;
    private Quaternion defaultRotation;
    private void Start()
    {
        defaultDirection = enemyPrefab.movementDirection;
        defaultRotation = enemyPrefab.transform.rotation;
    }
    public static float[] TimerEnemies(float total, int count)
    {
        float[] result = new float[count];
        BinarySplit(0f, total, 0, count, result);
        if (count == 1)
        {
            result[0] = UnityEngine.Random.Range(0f, total);
        }
        return result;
    }

    private static int BinarySplit(float start, float end, int index, int count, float[] arr)
    {
        if (count == 1)
        {
            arr[index] = end - start;
            return index + 1;
        }

        float split = UnityEngine.Random.Range(start, end);
        int leftCount = count / 2;
        int rightCount = count - leftCount;

        index = BinarySplit(start, split, index, leftCount, arr);
        index = BinarySplit(split, end, index, rightCount, arr);
        return index;
    }

    public IEnumerator SpawnEnemies(int count, float time, Vector3 lineStart, Vector3 lineEnd)
    {
        float[] timer = TimerEnemies(time, count);
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(timer[i]);
            float t = UnityEngine.Random.Range(0f, 1f);
            Vector3 startPosition = Vector3.Lerp(lineStart, lineEnd, t);
            Enemy enemy = Instantiate(enemyPrefab, startPosition, enemyPrefab.transform.rotation);
            if (location == MasterSpawner.SpawnLocation.Top)
            {
                enemy.movementDirection.y = -defaultDirection.y;
                enemy.transform.rotation = defaultRotation * Quaternion.Euler(0f, 0f, 180f);
            }
        }
        if (location == MasterSpawner.SpawnLocation.Top)
        {
            location = MasterSpawner.SpawnLocation.Bottom;
        }
        else if (location == MasterSpawner.SpawnLocation.Bottom)
        {
            location = MasterSpawner.SpawnLocation.Top;
        }
    }
}
