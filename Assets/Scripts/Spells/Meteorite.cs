using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : Spell
{
    [SerializeField] private GameObject meteoriteObjectPrefab;
    [SerializeField] private float spawnRadius = 5f;

    public override void Cast()
    {
        castEvent?.Invoke();
        canCast = false;

        Vector3 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(meteoriteObjectPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
