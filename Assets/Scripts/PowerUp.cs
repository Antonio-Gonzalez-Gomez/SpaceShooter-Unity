using UnityEngine;

public class PowerUp : EnemyBase
{
    [SerializeField] public UpgradeType type;

    public enum UpgradeType
    {
        Damage,
        AttackSpeed,
        MovementSpeed
    }
    override protected void Shoot()
    {

    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
