using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : Spell
{
    [SerializeField] private GameObject chainLightningPrefab;

    public override void Cast()
    {
        castEvent?.Invoke();
        canCast = false;
        Instantiate(chainLightningPrefab, transform.position, Quaternion.identity);
    }
}
