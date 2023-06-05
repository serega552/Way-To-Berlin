using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip[] _painClips;
    [SerializeField] private PainScreen _painScreen;
    [SerializeField] private Weapon[] _autoWeapon;
    [SerializeField] private Weapon[] _sniperWeapon;

    private Weapon _weapon1;
    private Weapon _weapon2;
    private AudioSource _audioSourse;
    private float _health = 3;
    private float _currentHealth = 3;
    private int _countPain = 0;
    private PlayerStatistics _localPlayerData = new PlayerStatistics();

    public event UnityAction<float> HealthChanged;

    private void Awake()
    {
        _audioSourse = GetComponent<AudioSource>();
        _currentHealth = _health;
        HealthChanged?.Invoke(_health);

        _localPlayerData = GlobalControl.Instance.SavedPlayerData;
        SelectGuns();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeepOneWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeepTwoWeapon();
        }
    }

    public void SelectGuns()
    {
        if (_localPlayerData.WeaponName1 == null)
        {
            _localPlayerData.WeaponName1 = "Ppsh";
        }

        if (_localPlayerData.WeaponName2 == null)
        {
            _localPlayerData.WeaponName2 = "Mosin";
        }

        foreach (Weapon weapon in _autoWeapon)
        { 
            if (weapon.NameGun == _localPlayerData.WeaponName1)
            {
                _weapon1 = weapon;
            }
        }

        foreach (Weapon weapon in _sniperWeapon)
        {
            if (weapon.NameGun == _localPlayerData.WeaponName2)
            {
                _weapon2 = weapon;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _audioSourse.PlayOneShot(_painClips[_countPain]);
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth);
        _countPain++;

        _painScreen.Open();

        if (_countPain >= _painClips.Length)
            _countPain = 0;
    }

    public void KeepOneWeapon()
    {
        if (_weapon1 != null && _weapon2 != null)
        {
            _weapon1.gameObject.SetActive(true);
            _weapon2.gameObject.SetActive(false);
        }
    }

    public void KeepTwoWeapon()
    {
        if (_weapon1 != null && _weapon2 != null)
        {
            _weapon2.gameObject.SetActive(true);
            _weapon1.gameObject.SetActive(false);
        }
    }

    public void GetMoney(int money)
    {
        _localPlayerData.Money += money;
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.SavedPlayerData = _localPlayerData;
    }
}
