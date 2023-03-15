using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtonUI : MonoBehaviour
{
    [SerializeField] private Image reloadImage;
    [SerializeField] private Spell spell;

    private void Start()
    {
        reloadImage.fillAmount = 0;
        spell.castEvent.AddListener(SetReloadImage);
    }

    private void Update()
    {
        if (spell.CanCast() == false)
        {
            reloadImage.fillAmount -= 1 / spell.GetCooldownTimer() * Time.deltaTime;
        }
    }

    private void SetReloadImage()
    {
        reloadImage.fillAmount = 1;
    }
}
