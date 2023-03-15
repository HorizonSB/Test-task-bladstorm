using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 10;
    [SerializeField] private int autoAttackDamage = 2;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float attackSpeed = 1;
    [SerializeField] private float attackRadius = 0.4f;

    [SerializeField] private bool isBoss = false;

    private float attackCountdown;
    private Transform target;

    private const string ENEMY_TAG = "Enemy";
    private const string ALLY_TAG = "Ally";

    private Rigidbody2D _rigidbody;
    private Vector3 localScale;

    public UnityEvent OnAttackPlayer = new UnityEvent();
    public UnityEvent OnAttackAlly = new UnityEvent();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        TargetPlayer();
    }

    public void TargetPlayer()
    {
        target = Player.Instance.transform;
        gameObject.tag = ENEMY_TAG;
        OnAttackPlayer?.Invoke();
    }

    public void TargetAlly()
    {
        gameObject.tag = ALLY_TAG;
        OnAttackAlly?.Invoke();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void TargetAttacking(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (!target && CompareTag(ENEMY_TAG)) TargetPlayer();
        if (!target) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (attackCountdown <= 0 && distanceToTarget <= attackRadius)
        {
            Attack();
            attackCountdown = 1f / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;
    }

    private void Attack()
    {
        if (target.gameObject.CompareTag(ENEMY_TAG))
        {
            target.GetComponent<Enemy>().TargetAttacking(gameObject.transform);
        }
        target.GetComponent<IDamageable>().GetDamage(autoAttackDamage);
    }

    public bool IsBoss()
    {
        return isBoss;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Move();
        }
        else
        {
            StopMove();
        }

    }

    private void Move()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        _rigidbody.velocity = new Vector2(directionToTarget.x, directionToTarget.y) * speed;
    }

    private void StopMove()
    {
        _rigidbody.velocity = new Vector2(0f, 0f);
    }

    public void GetDamage(int damage)
    {
        if (Bloodlust.Instance.IsBloodlustActive())
        {
            float bloodlustProcent = 0.05f;
            health -= damage + health * bloodlustProcent;
            Player.Instance.Heal(health * bloodlustProcent);
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            EnemySpawnManager.Instance.OnEnemyKilled?.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateTarget()
    {
        if(gameObject.tag == ALLY_TAG)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);

            float shortetsDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortetsDistance)
                {
                    shortetsDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
