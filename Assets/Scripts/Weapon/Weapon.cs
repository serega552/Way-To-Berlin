using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected AudioClip ShootClip;
    [SerializeField] protected AudioSource AudioSourse;
    [SerializeField] protected ParticleSystem ShootEffect;
    [SerializeField] protected Camera MainCamera;
    [SerializeField] private ShootButton _shootButton;
    [SerializeField] protected Game Game;

    protected float Range = 13f;
    protected float Force = 10f;
    protected float FireRate = 0.4f;
    protected float NextFire = 0f;
    protected Animator Animator;
    public ShootButton ShootButton => _shootButton;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        AudioSourse= GetComponent<AudioSource>();
    }

    public abstract void Shoot();
}
