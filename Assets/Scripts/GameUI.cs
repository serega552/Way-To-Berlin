using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private PauseScreen _pause;

    private void Start()
    {
        _pause.Open();
    }

    public void Win()
    {
        _winScreen.Open();
    }
}
