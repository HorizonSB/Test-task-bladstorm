using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        enemy.OnAttackAlly.AddListener(AttackAlly);
        enemy.OnAttackPlayer.AddListener(AttackPlayer);
    }

    private void AttackPlayer()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1f);
    }

    private void AttackAlly()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 1f, 1f);
    }
}
