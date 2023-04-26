using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Point _point;

    private float _health = 100f;    
    private float _currentHealth;
    private float _damage = 1f;

    public float Damage => _damage;

    public event UnityAction<float,float> HealthChanged;

    private void Start()
    {
        _currentHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _point.DieEnemy();

        if(gameObject.TryGetComponent<Sniper>(out Sniper sniper))
        {
            sniper.TurnOffImage();
        }

        gameObject.SetActive(false);
    }
}
