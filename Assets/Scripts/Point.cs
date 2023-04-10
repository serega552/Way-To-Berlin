using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private EnemyController[] _enemyControllers;
    [SerializeField] private Sniper[] _snipers;
    [SerializeField] private PlayerController _playerController;

    private int _countEnemy;

    private void Start()
    {
        TakePointPosition();

        foreach(var item in _enemyControllers)
        {
            _countEnemy++;
        }

        foreach(var item in _snipers)
        {
            _countEnemy++;
        }
    }

    public void TakePointPosition()
    {
        foreach (var item in _enemyControllers)
        {
            item.GetPointPosition(transform);
        }
    }

    public void LetEnemiesAttack()
    {
        foreach (var item in _enemyControllers)
        {
            item.Move();
            item.GetComponent<Animator>().SetBool("Run", true);
        }
        
        foreach (var item in _snipers)
        {
            item.Attack();
        }
    }

    public void MovePlayer()
    {
        if (_countEnemy == 0 && _playerController.transform.position == transform.position)
        {
            _playerController.Continue();
        }
        else
        {

        }
    }

    public void DieEnemy()
    {
        _countEnemy--;
    }
}
