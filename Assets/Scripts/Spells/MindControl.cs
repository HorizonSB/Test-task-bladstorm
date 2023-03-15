using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControl : Spell
{
    [SerializeField] private float timeUnderControl = 5f;

    private const string ENEMY_TAG = "Enemy";


    public override void Cast()
    {


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);

        if(enemies.Length != 0)
        {
            GameObject enemyUnderControl = enemies[Random.Range(0, enemies.Length)];
            if (!enemyUnderControl.GetComponent<Enemy>().IsBoss())
            {
                castEvent?.Invoke();
                canCast = false;
                enemyUnderControl.GetComponent<Enemy>().TargetAlly();
                StartCoroutine(MindControlCoroutine(enemyUnderControl));
            }
            else
            {
                Cast();
            }
        }
    }

    private IEnumerator MindControlCoroutine(GameObject enemyUnderControl)
    {
        yield return new WaitForSeconds(timeUnderControl);
        if(enemyUnderControl != null) enemyUnderControl.GetComponent<Enemy>().TargetPlayer();
    }
}

