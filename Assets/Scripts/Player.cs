using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 300;
    [SerializeField] private float fireRate = 2;
    [SerializeField] private float attackRange = 4f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private HealthBar healthBar;

    private float fireCountdown;
    private const string ENEMY_TAG = "Enemy";
    private Transform target;

    public static Player Instance { get; private set; }

    public UnityEvent GameOverEvent = new UnityEvent();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);

        float shortetsDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortetsDistance)
            {
                shortetsDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortetsDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (!target) return;

        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Vector2 lookDir = target.position - gameObject.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().rotation = angle;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void Heal(float health)
    {
        currentHealth += health;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    private void GameOver()
    {
        GameOverEvent?.Invoke();
        Time.timeScale = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
