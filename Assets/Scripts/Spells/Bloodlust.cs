using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodlust : Spell
{
    [SerializeField] private bool isBloodlustActive = false;

    public static Bloodlust Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Cast()
    {
        isBloodlustActive = true;
    }

    public bool IsBloodlustActive()
    {
        return isBloodlustActive;
    }
}

