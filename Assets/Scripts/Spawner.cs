using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] EnemyBase enemyPrefab;
    [SerializeField] public int enemyCost;
    [SerializeField] public MasterSpawner.SpawnLocation location;

    public static float[] TimerEnemies(float total, int count)
    {
        float[] result = new float[count];
        SplitRange(0f, total, 0, count, result);
        return result;
    }

    private static int SplitRange(float start, float end, int index, int count, float[] arr)
    {
        if (count == 1)
        {
            arr[index] = end - start;
            return index + 1;
        }

        float split = UnityEngine.Random.Range(start, end);
        int leftCount = count / 2;
        int rightCount = count - leftCount;

        index = SplitRange(start, split, index, leftCount, arr);
        index = SplitRange(split, end, index, rightCount, arr);
        return index;
    }

    public IEnumerator SpawnEnemies(int count, float time, Vector3 lineStart, Vector3 lineEnd)
    {
        float[] timer = TimerEnemies(time, count);
        for (int i = 0; i < count; i++)
        {
            if (count == 1)
            {
                timer[0] = UnityEngine.Random.Range(0f, time);
            }
            yield return new WaitForSeconds(timer[i]);
            float t = UnityEngine.Random.Range(0f, 1f);
            Vector3 startPosition = Vector3.Lerp(lineStart, lineEnd, t);
            EnemyBase enemy = Instantiate(enemyPrefab, startPosition, enemyPrefab.transform.rotation);
            if (location == MasterSpawner.SpawnLocation.Top)
            {
                enemy.movementDirection.y = -enemy.movementDirection.y;
                enemy.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
                location = MasterSpawner.SpawnLocation.Bottom;
            }
            if (location == MasterSpawner.SpawnLocation.Bottom)
                location = MasterSpawner.SpawnLocation.Top;
        }
    }
}
