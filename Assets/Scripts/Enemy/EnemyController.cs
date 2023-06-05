using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private Transform _path;
    [SerializeField] private Point _endPoint;

    private int _currentPoint = 0;
    private bool _isMoving = false;
    private bool _isCoroutineAttackRunning = false;
    private Enemy _enemy;
    private Coroutine _attackCoroutine;
    private Transform[] _points;
    private AudioSource _audioSourse;
    private WaitForSeconds _waitAttack = new WaitForSeconds(1f);

    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
        _enemy = GetComponent<Enemy>();

        if (_path != null)
        {
            _points = new Transform[_path.transform.childCount];
            for (int i = 0; i < _path.transform.childCount; i++)
            {
                _points[i] = _path.transform.GetChild(i);
            }
        }
    }

    private void Update()
    {
        if (_isMoving && _path != null && _endPoint == null)
        {
            if (_currentPoint < _points.Length)
            {
                Vector3 lookDirection = _points[_currentPoint].position - transform.position;

                transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _speed * Time.deltaTime);

                if (transform.position == _points[_currentPoint].position)
                    _currentPoint++;
            }
            else if (_isCoroutineAttackRunning == false)
            {
                _attackCoroutine = StartCoroutine(DelayBeforeAttack());
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
            }
        }

        if(_isMoving && _path == null && _endPoint != null)
        {
            float distance = Vector3.Distance(transform.position, _endPoint.transform.position);

            Vector3 lookDirection = _endPoint.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            if(distance > 1f)
            {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.transform.position, _speed * Time.deltaTime);
            }
            else if (_isCoroutineAttackRunning == false)
            {
                _attackCoroutine = StartCoroutine(DelayBeforeAttack());
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
            }
        }

        if(gameObject.GetComponent<Sniper>() == null && _path == null && _endPoint == null)
        {
            _enemy.Die();
        }
    }

    private IEnumerator DelayBeforeAttack()
    {
        while (true)
        {
            yield return _waitAttack;

            _player.TakeDamage(_enemy.Damage);
            gameObject.GetComponent<Enemy>().Die();
            StopAttackCoroutine();

            yield return null;
        }
    }

    public void Move()
    {
        _isMoving = true;
    }

    private void StopAttackCoroutine()
    {
        StopCoroutine(_attackCoroutine);
    }
}
