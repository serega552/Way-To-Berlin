using System.Collections;
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
        if (Input.GetButton("Fire1") && Game.IsMobile == false && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            StartCoroutine(ShootDelay());
        }

        if (ShootButton.IsHold && Game.IsMobile && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            StartCoroutine(ShootDelay());
        }

        if (Input.GetButtonDown("Fire2") && Game.IsMobile == false)
        {
            Zoom();
        }

        if (Input.GetKeyDown(KeyCode.R) && IsReloading == false)
        {
            IsReloading = true;
            StartCoroutine(ReloadDelay());
        }

        CurrentBullets.text = CountBulletSpent.ToString();
        MaxBullets.text = MaxBulletCount.ToString();
    }

    public override void Reload()
    {
        if (gameObject.activeSelf)
            StartCoroutine(ReloadDelay());
    }

    public override void Shoot()
    {
        CountBulletSpent--;

        if (CountBulletSpent >= 0)
        {
            RaycastHit hit;

            AudioSourse.PlayOneShot(ShootClip);

            Animator.SetTrigger("shoot");
         
            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, Range) && _isZoom == false)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                Explosion explosion = hit.transform.GetComponent<Explosion>();
                Head head = hit.transform.GetComponent<Head>();

                if (enemy != null && head == null)
                {
                    enemy.TakeDamage(Damage);
                }

                if(head != null)
                {
                    head.GetComponentInParent<Enemy>().TakeDamage(Damage * 2);
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
                Head head = hit.transform.GetComponent<Head>();

                if (enemy != null && head == null)
                {
                    enemy.TakeDamage(Damage);
                }

                if (head != null)
                {
                    head.GetComponentInParent<Enemy>().TakeDamage(Damage * 2);
                }

                if (explosion != null)
                {
                    explosion.ExplodeDelay();
                }
            }
        }
        else if(CountBulletSpent < 0)
        {
            IsReloading = true;
            StartCoroutine(ReloadDelay());
        }

        IsShooting= false;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1.37f);
        AudioSourse.PlayOneShot(ShootClip);
        Animator.SetTrigger("shoot");
        Shoot();
    }

    private IEnumerator ReloadDelay()
    {
        Animator.SetTrigger("reload");
        CountBulletSpent = MaxBulletCount;
        yield return new WaitForSeconds(1.8f);
        IsReloading = false;
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
