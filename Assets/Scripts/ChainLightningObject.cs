using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningObject : MonoBehaviour
{
    [SerializeField] private int damage = 7;
    [SerializeField] private float timeTillNextHop = 0.3f;

    private LineRenderer line;
    private const string ENEMY_TAG = "Enemy";
    private int maxHops = 6;

    private Enemy target;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        Attack();
    }

    private void Update()
    {
        if (!target) Destroy(gameObject);
    }

    private void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        List<Transform> enemiesTransform = new List<Transform>();
        int counter = 0;

        foreach(GameObject enemy in enemies)
        {
            if(counter < maxHops)
            {
                counter++;
                enemiesTransform.Add(enemy.transform);
            }
        }

        if(enemiesTransform.Count > 0)
        {
            StartCoroutine(LigthningHops(enemiesTransform));
        }
    }

    private IEnumerator LigthningHops(List<Transform> enemiesTransform)
    {
        int enemiesAmount = enemiesTransform.Count;
        line.positionCount = enemiesTransform.Count;

        for (int i =0; i < enemiesAmount; i++)
        {
            Transform randomEnemy = enemiesTransform[Random.Range(0, enemiesTransform.Count)];
            if (randomEnemy)
            {
                target = randomEnemy.GetComponent<Enemy>();
            }
            else
            {
                Destroy(gameObject);
            }


            line.SetPosition(i, target.transform.position);
            target.GetDamage(damage);
            enemiesTransform.Remove(target.transform);
            yield return new WaitForSeconds(timeTillNextHop);
        }
        Destroy(gameObject);
    }
}
