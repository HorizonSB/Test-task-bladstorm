using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spell : MonoBehaviour, ISpell
{
    [SerializeField] protected float cooldown = 5f;

    protected bool canCast = true;

    protected float timer;

    public UnityEvent castEvent = new UnityEvent();

    public bool CanCast()
    {
        return canCast;
    }

    public virtual void Cast()
    {
        Debug.Log("Cast spell");
    }

    public float GetCooldownTimer()
    {
        return timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Cast();
            timer = cooldown;
        }
    }
}
