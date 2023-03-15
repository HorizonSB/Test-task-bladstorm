using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 5;

    private float distanceToDestroy = 10f;
    private const string ENEMY_TAG = "Enemy";
    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        DestoyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }

    private void DestoyProjectile()
    {
        float distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);
        if (distanceToPlayer > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
