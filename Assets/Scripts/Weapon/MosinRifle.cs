using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MosinRifle : Weapon
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _mosinCamera;
    [SerializeField] private Camera _chapterCamera;
    [SerializeField] private CanvasGroup _buttons; 

    private bool _isZoom = false;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Game.IsMobile == false && Time.time > NextFire)
        {
            Debug.Log("fdgf");
            NextFire = Time.time + 1.2f / FireRate;
            Shoot();
        }

        if (ShootButton.IsHold && Game.IsMobile && Time.time > NextFire)
        {
            NextFire = Time.time + 1.2f / FireRate;
            Shoot();
        }

        if (Input.GetButtonDown("Fire2") && Game.IsMobile == false)
        {
            Zoom();
        }
    }

    public override void Shoot()
    {
        RaycastHit hit;

        AudioSourse.PlayOneShot(ShootClip);

        Animator.SetTrigger("shoot");

        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, Range) && _isZoom == false)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Explosion explosion = hit.transform.GetComponent<Explosion>();

            if (enemy != null)
            {
                hit.rigidbody.AddForce(-hit.normal * Force);
                enemy.TakeDamage(Damage);
            }

            if (explosion != null)
            {
                explosion.ExplodeDelay();
            }
        }

        else if (Physics.Raycast(_mosinCamera.transform.position, _mosinCamera.transform.forward, out hit, Range * 100f) && _isZoom)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Explosion explosion = hit.transform.GetComponent<Explosion>();

            if (enemy != null)
            {
                hit.rigidbody.AddForce(-hit.normal * Force);
                enemy.TakeDamage(Damage);
            }

            if (explosion != null)
            {
                explosion.ExplodeDelay();
            }
        }
    }

    public void Zoom()
    {
        if (_isZoom == false)
        {
            OnZoom();
            _buttons.alpha = 0f;
        }

        else if (_isZoom)
        {
            OffZoom();
            _buttons.alpha = 1f;
        }

    }

    private void OnZoom()
    {
        _isZoom = true;
        _mosinCamera.gameObject.SetActive(true);
        _chapterCamera.enabled = false;

        _chapterCamera.GetComponent<FirstPersonLook>().ChangeSensivity(_isZoom);
    }

    private void OffZoom()
    {
        _isZoom = false;
        _mosinCamera.gameObject.SetActive(false);
        _chapterCamera.enabled = true;

        _chapterCamera.GetComponent<FirstPersonLook>().ChangeSensivity(_isZoom);
    }
}
