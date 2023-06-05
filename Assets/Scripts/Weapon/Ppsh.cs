using System.Collections;
using UnityEngine;

public class Ppsh : Weapon
{
    [SerializeField] private Player _player;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Game.IsMobile == false && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            StartCoroutine(ShootDelay());
        }

        if(ShootButton.IsHold && Game.IsMobile && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            StartCoroutine(ShootDelay());
        }

        if (Input.GetKeyDown(KeyCode.R) && IsReloading == false)
        {
            IsReloading= true;
            StartCoroutine(ReloadDelay());
        }

        CurrentBullets.text = CountBulletSpent.ToString();
        MaxBullets.text = MaxBulletCount.ToString();
    }

    public override void Reload()
    {
        if(gameObject.activeSelf) 
            StartCoroutine(ReloadDelay());
    }

    public override void Shoot()
    {
        CountBulletSpent--;

        if (CountBulletSpent >= 0)
        {

            RaycastHit hit;

            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, Range))
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
                    head.GetComponentInParent<Enemy>().TakeDamage(Damage);
                }

                if (explosion != null)
                {
                    explosion.ExplodeDelay();
                }
            }
        }
        else if(CountBulletSpent < 0)
        {
            IsReloading= true;
            StartCoroutine(ReloadDelay());
        }

        IsShooting = false;
    }
    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.1f);
        AudioSourse.PlayOneShot(ShootClip);
        Animator.SetTrigger("shoot");
        Shoot();
    }

    private IEnumerator ReloadDelay()
    {
        Animator.SetTrigger("reload");
        CountBulletSpent = MaxBulletCount;
        yield return new WaitForSeconds(1.41f);
        IsReloading = false;
    }
}
