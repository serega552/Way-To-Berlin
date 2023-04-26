using UnityEngine;

public class MosinRifle : Weapon
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _mosinCamera;
    [SerializeField] private Camera _chapterCamera;
    [SerializeField] private CanvasGroup _buttons;

    private float _timer;
    private bool _isZoom = false;
    private int _maxBullet = 5;
    private int _countBulletSpent = 0;

    private void Awake()
    {
        _countBulletSpent = _maxBullet;
    }

    private void Update()
    {
        _timer = Time.time;

        if (Input.GetButton("Fire1") && Game.IsMobile == false && _timer > NextFire)
        {
            NextFire = _timer + 0.7f / FireRate;
            Shoot();
        }

        if (ShootButton.IsHold && Game.IsMobile && _timer > NextFire)
        {
            NextFire = _timer + 0.7f / FireRate;
            Shoot();
        }

        if (Input.GetButtonDown("Fire2") && Game.IsMobile == false)
        {
            Zoom();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Animator.SetTrigger("reload");
            _countBulletSpent = _maxBullet;
        }

        CurrentBullets.text = _countBulletSpent.ToString();
        MaxBullets.text = _maxBullet.ToString();
    }

    public override void Shoot()
    {
        _countBulletSpent--;

        if (_countBulletSpent >= 0)
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
        else if(_countBulletSpent < 0)
        {
            Animator.SetTrigger("reload");
            _countBulletSpent = _maxBullet;
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
