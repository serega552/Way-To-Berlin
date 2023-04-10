using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon _weapon1;
    [SerializeField] Weapon _weapon2;

    private float _health = 3;
    private float _currentHealth = 3;

    public event UnityAction<float> HealthChaged;

    private void Start()
    {
        _currentHealth = _health;
        HealthChaged?.Invoke(_health);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            KeepOneWeapon();
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            KeepTwoWeapon();
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthChaged?.Invoke(_currentHealth);
    }

    public void KeepOneWeapon()
    {
        _weapon1.gameObject.SetActive(true);
        _weapon2.gameObject.SetActive(false);
    }

    public void KeepTwoWeapon()
    {
        _weapon2.gameObject.SetActive(true);
        _weapon1.gameObject.SetActive(false);
    }
}
