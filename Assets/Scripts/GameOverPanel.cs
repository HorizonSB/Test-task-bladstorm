using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject container;

    private void Start()
    {
        Hide();
        Player.Instance.GameOverEvent.AddListener(Show);
    }

    private void Show()
    {
        container.SetActive(true);
    }

    private void Hide()
    {
        container.SetActive(false);
    }
}
