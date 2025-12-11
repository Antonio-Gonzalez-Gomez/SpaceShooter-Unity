using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static PowerUp;
public class PlayerSpaceShip : MonoBehaviour
{

    [SerializeField] UIGameOver ui;
    [Header("Movement")]
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float acceleration = 2f;
    [Header("Shooting")]
    [SerializeField] BulletBase lowDmgPrefab;
    [SerializeField] BulletBase mediumDmgPrefab;
    [SerializeField] BulletBase highDmgPrefab;
    [SerializeField] float shootingPeriod = 1f;
    [Header("Controls")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;
    [Header("Effects")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] GameObject destroyEffect;

    private Rigidbody2D rb;
    private BulletBase currentBullet;
    private AudioSource audioSource;
    public List<UpgradeType> remainingPowerUps;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingPowerUps = new List<UpgradeType>();
        initPowerUps();
        currentBullet = lowDmgPrefab;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void initPowerUps()
    {
        remainingPowerUps.Add(UpgradeType.Damage);
        remainingPowerUps.Add(UpgradeType.Damage);
        remainingPowerUps.Add(UpgradeType.AttackSpeed);
        remainingPowerUps.Add(UpgradeType.AttackSpeed);
        remainingPowerUps.Add(UpgradeType.MovementSpeed);
        remainingPowerUps.Add(UpgradeType.MovementSpeed);
    }
    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;
    }

    private Vector2 rawMove;
    private float shotCooldown = 0f;

    void Update()
    {
        shotCooldown -= Time.deltaTime;
        if (shoot.action.IsPressed() && shotCooldown <= 0f)
        {
            Instantiate(currentBullet, transform.position, Quaternion.identity);
            shotCooldown = shootingPeriod;
            audioSource.PlayOneShot(shootSFX);
        }

        Vector2 targetVelocity = rawMove * maxSpeed;
        Vector2 force = (targetVelocity - rb.linearVelocity) * acceleration;
        rb.AddForce(force);
        transform.rotation = Quaternion.identity;
    }

    private void OnDisable()
    {
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        move.action.Disable();
        shoot.action.Disable();
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }

    private void ActivatePowerUp(UpgradeType tipo)
    {
        switch (tipo)
        {
            case UpgradeType.Damage:
                if (currentBullet.damage == 2)
                {
                    currentBullet = highDmgPrefab;
                }
                if (currentBullet.damage == 1)
                {
                    currentBullet = mediumDmgPrefab;
                }
                break;
            case UpgradeType.AttackSpeed:
                shootingPeriod -= 0.25f;
                break;
            case UpgradeType.MovementSpeed:
                maxSpeed += 0.5f;
                acceleration += 0.5f;
                break;
        }
        audioSource.PlayOneShot(powerUpSFX);
        remainingPowerUps.Remove(tipo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            ui.ShowGameOver();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            shotCooldown = 9999f;
        }
        if (collision.CompareTag("PowerUp"))
        {
            ActivatePowerUp(collision.gameObject.GetComponent<PowerUp>().type);
            Destroy(collision.gameObject);
        }
    }

}
