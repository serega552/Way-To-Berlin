using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(AudioSource))]
public class Sniper : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private Image _noticeFire;

    private PlayerController _playerController;
    private Animator _animator;
    private AudioSource _audioSourse;
    private Enemy _enemy;
    private bool _canAttack = true;
    private float _lerpDuration = 3f;

    private void Start()
    {
        _animator= GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _audioSourse= GetComponent<AudioSource>();
        _playerController = _player.GetComponent<PlayerController>();
        TurnOffImage();
    }

    public void Attack()
    {
        TurnOnImage();
        StartCoroutine(AttackCoroutine(1, 0, _lerpDuration));
    }

    private IEnumerator AttackCoroutine(float startValue, float endValue, float duration)
    {

        while (_canAttack)
        {
            float elapsed = 0;
            float nextValue;

            while (elapsed < duration)
            {
                nextValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
                _noticeFire.fillAmount = nextValue;
                elapsed += Time.deltaTime;
                yield return null;
            }

            if (_playerController)
            {
                _audioSourse.PlayOneShot(_audioClip);
                _animator.SetTrigger("shootSniper");

                if (_playerController.IsHide == false)
                {
                    _player.TakeDamage(_enemy.Damage);
                    Return();
                }
            }
        }
    }

    public void TurnOnImage()
    {
        _noticeFire.fillAmount = 1f;
        _noticeFire.gameObject.SetActive(true);
    }

    private void Return()
    {
        _noticeFire.fillAmount = 1f;
    }

    public void TurnOffImage()
    {
        _noticeFire.fillAmount = 1f;
        _noticeFire.gameObject.SetActive(false);
    }
}
