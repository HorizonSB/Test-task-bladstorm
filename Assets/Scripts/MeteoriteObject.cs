using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteObject : MonoBehaviour
{
    [SerializeField] private int damage = 100;

    private int damagePerUnit = 10;

    private const string ENENY_TAG = "Enemy";
    private int collisionsCount;
    private bool causedDamage = false;

    private List<Enemy> enemyList = new List<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionsCount++;

        if (collision.CompareTag(ENENY_TAG))
        {
            enemyList.Add(collision.GetComponent<Enemy>());
        }
        if (!causedDamage)
        {
            causedDamage = true;
            Invoke("Damage", 0.5f);
        }

    }

    private void Damage()
    {
        foreach(Enemy enemy in enemyList)
        {
            enemy.GetDamage(damage + collisionsCount * damagePerUnit);
        }

        Destroy(gameObject);
    }
}
