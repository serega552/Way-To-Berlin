using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private EnemyController[] _enemyControllers;
    [SerializeField] private Sniper[] _snipers;
    [SerializeField] private PlayerController _playerController;

    private int _countEnemy;

    public PlayerController PlayerController => _playerController;
    public int CountEnemy => _countEnemy;

    private void Start()
    {
        foreach(var item in _enemyControllers)
        {
            _countEnemy++;
        }

        foreach(var item in _snipers)
        {
            _countEnemy++;
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

    public void DieEnemy()
    {
        _countEnemy--;

        if (_countEnemy == 0)
            _playerController.Continue();
    }
}
