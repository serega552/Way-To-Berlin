using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;

    private Enemy _enemy;
    private Transform _pointPosition;
    private bool _isMoving = false;
    private Coroutine _attackCoroutine;
    private bool _isCoroutineAttackRunning = false;
    private WaitForSeconds _waitAttack = new WaitForSeconds(1f);

    private void Start()
    {
        _enemy= GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            float distance = Vector3.Distance(transform.position, _pointPosition.transform.position);

            if (distance > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pointPosition.position, _speed * Time.deltaTime);
            }
            else if (_isCoroutineAttackRunning == false)
            {
                _attackCoroutine = StartCoroutine(DelayBeforeAttack());
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }

    public void GetPointPosition(Transform target)
    {
        _pointPosition = target;
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
