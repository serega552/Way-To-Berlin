using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(AudioSource))]
public class Sniper : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private ParticleSystem _shootEffect;

    private PlayerController _playerController;
    private Animator _animator;
    private AudioSource _audioSourse;
    private Coroutine _attackCoroutine;
    private bool _isCoroutineAttackRunning = false;
    private WaitForSeconds _waitAttack = new WaitForSeconds(3f);
    private Enemy _enemy;
    private bool _canAttack = true;

    private void Start()
    {
        _animator= GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _audioSourse= GetComponent<AudioSource>();
        _playerController = _player.GetComponent<PlayerController>();
    }

    public void Attack()
    {
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (_canAttack)
        {
            yield return _waitAttack;
            if (_playerController)
            {
                _audioSourse.PlayOneShot(_audioClip);
                _animator.SetTrigger("shootSniper");

                if (_playerController.IsHide == false)
                    _player.TakeDamage(_enemy.Damage);
            }
            yield return _waitAttack;
        }
    }
}
