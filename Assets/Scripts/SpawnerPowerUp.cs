using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static PowerUp;

public class SpawnerPowerUp : MonoBehaviour
{
    [SerializeField] PowerUp damagePrefab;
    [SerializeField] PowerUp atkspeedPrefab;
    [SerializeField] PowerUp movementPrefab;
    private PlayerSpaceShip player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpaceShip>();
    }

    public void SpawnPowerUp(Vector3 lineStart, Vector3 lineEnd)
    {
        List<UpgradeType> remainingPowerUps = player.remainingPowerUps;
        if (remainingPowerUps.Count > 0)
        {
            float t = UnityEngine.Random.Range(0f, 1f);
            Vector3 startPosition = Vector3.Lerp(lineStart, lineEnd, t);
            UpgradeType tipo = remainingPowerUps[UnityEngine.Random.Range(0, remainingPowerUps.Count - 1)];
            switch (tipo)
            {
                case UpgradeType.Damage:
                    Instantiate(damagePrefab, startPosition, Quaternion.identity);
                    break;
                case UpgradeType.AttackSpeed:
                    Instantiate(atkspeedPrefab, startPosition, Quaternion.identity);
                    break;
                case UpgradeType.MovementSpeed:
                    Instantiate(movementPrefab, startPosition, Quaternion.identity);
                    break;
            }
        }
    }

}
