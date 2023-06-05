using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _priceWeapon;
    [SerializeField] private Sprite _currentWeapon;
    [SerializeField] private TMP_Text _price;

    private WeaponSelection _weaponSelection;
    private bool _isBought = false;
    private bool _isEquipped = false;
    private string _bought = "Bought";
    private PlayerStatistics _localPlayerData = new PlayerStatistics();

    public int PriceWeapon => _priceWeapon;
    public bool IsBought => _isBought;
    public bool IsEquipped => _isEquipped;
    public Sprite CurrentWeapon => _currentWeapon;
    public string Name => _name;

    private void Awake()
    {
        CheckData();

        _localPlayerData = GlobalControl.Instance.SavedPlayerData;
        _weaponSelection = GetComponentInParent<WeaponSelection>();
        _price.text = _priceWeapon.ToString();
    }

    public void OnButtonClick()
    {
        _weaponSelection.SelectWeapon(this);
    }

    public void Buy()
    {
        _isBought = true;
        _priceWeapon = 0;
        _price.text = _bought;

        PlayerPrefs.SetInt(Name + _bought, 1);
    }

    public void Equip(string typeWeapon, string auto, string sniper)
    {
        _isEquipped = true;

        if (typeWeapon == auto)
            _localPlayerData.WeaponName1 = Name;
        else if (typeWeapon == sniper)
            _localPlayerData.WeaponName2 = Name;

        SavedStatistics();
    }

    public void UnEquip(string typeWeapon, string auto, string sniper)
    {
        _isEquipped = false;

        if (typeWeapon == auto)
            _localPlayerData.WeaponName1 = Name;
        else if (typeWeapon == sniper)
            _localPlayerData.WeaponName2 = Name;

        SavedStatistics();
    }

    private void CheckData()
    {
        if (PlayerPrefs.GetInt(Name + _bought) == 1)
        {
            _isBought = true;
            _priceWeapon = 0;
            _price.text = _bought;
        }
    }

    private void SavedStatistics()
    {
        GlobalControl.Instance.SavedPlayerData = _localPlayerData;
    }
}
