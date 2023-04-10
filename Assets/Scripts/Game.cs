using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Point _path;
    [SerializeField] private bool _isMobile;
    [SerializeField] private PauseScreen _pause;

    public bool IsMobile => _isMobile;

    private Transform[] _points;
    private int _currentPoint = -1;

    private void Start()
    {
        _pause.Open();

        _points = new Transform[_path.transform.childCount];

        for (int i = 0; i < _path.transform.childCount; i++)
        {
            _points[i] = _path.transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (_currentPoint < _points.Length)
        {
            _points[_currentPoint].GetComponent<Point>().MovePlayer();
        }
    }

    public void AttackPlayer()
    {
        _points[_currentPoint].GetComponent<Point>().LetEnemiesAttack();
    }

    public Transform TakePointPosition()
    {
        if (_currentPoint < _points.Length - 1)
        {
            _currentPoint++;
            Transform target = _points[_currentPoint];
            return target;
        }
        else
        {
            _pause.Restart();
        }
        return null;
    }

    public bool TryMoving()
    {
        if (_currentPoint < _points.Length )
            return true;
        else
            return false;
    }
}
