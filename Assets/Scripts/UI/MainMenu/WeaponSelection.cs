using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private WeaponCard[] _autoWeaponCards;
    [SerializeField] private WeaponCard[] _sniperWeaponCards;
    [SerializeField] private Image _equipWeapon1;
    [SerializeField] private Image _equipWeapon2;

    private string _auto = "auto";
    private string _sniper = "sniper";
    private string _typeWeapon;
    private WeaponCard _currentWeaponCard;
    private PlayerStatistics _localPlayerData = new PlayerStatistics();

    public event UnityAction<int> OnChangeMoney;

    private void Awake()
    {
        _localPlayerData = GlobalControl.Instance.SavedPlayerData;
    }

    public void SelectWeapon(WeaponCard weaponCard)
    {
        _currentWeaponCard = weaponCard;
        if (_currentWeaponCard.GetComponent<AutoWeapon>() != null)
            _typeWeapon = _auto;
        else if (_currentWeaponCard.GetComponent<SniperWeapon>() != null)
            _typeWeapon = _sniper;

        if (_typeWeapon == _auto)
        {
            if (_localPlayerData.Money >= _currentWeaponCard.PriceWeapon && _currentWeaponCard.IsBought == false)
            {
                _equipWeapon1.sprite = _currentWeaponCard.CurrentWeapon;
                BuyWeapon();
            }
            else if (_currentWeaponCard.IsBought && _currentWeaponCard.IsEquipped == false)
            {
                TurnOffWeapons();
                _equipWeapon1.sprite = _currentWeaponCard.CurrentWeapon;
                _currentWeaponCard.Equip(_typeWeapon, _auto, _sniper);
            }
            else if (_currentWeaponCard.IsBought == false)
            {

            }
        }
        else if (_typeWeapon == _sniper)
        {
            if (_localPlayerData.Money >= _currentWeaponCard.PriceWeapon && _currentWeaponCard.IsBought == false)
            {
                _equipWeapon2.sprite = _currentWeaponCard.CurrentWeapon;
                BuyWeapon();

            }
            else if (_currentWeaponCard.IsBought && _currentWeaponCard.IsEquipped == false)
            {
                TurnOffWeapons();
                _equipWeapon2.sprite = _currentWeaponCard.CurrentWeapon;
                _currentWeaponCard.Equip(_typeWeapon, _auto, _sniper);
            }
            else if (_currentWeaponCard.IsBought == false)
            {

            }
        }

        SavedStatistics();
    }

    private void BuyWeapon()
    {
        _localPlayerData.Money -= _currentWeaponCard.PriceWeapon;

        TurnOffWeapons();

        _currentWeaponCard.Buy();
        _currentWeaponCard.Equip(_typeWeapon, _auto, _sniper);

        OnChangeMoney?.Invoke(_localPlayerData.Money);
    }

    private void TurnOffWeapons()
    {
        if (_typeWeapon == _sniper)
        {
            foreach (var weapon in _sniperWeaponCards)
            {
                weapon.UnEquip(_typeWeapon, _auto, _sniper);
            }
        }
        if (_typeWeapon == _auto)
        {
            foreach (var weapon in _autoWeaponCards)
            {
                weapon.UnEquip(_typeWeapon, _auto, _sniper);
            }
        }
    }

    private void SavedStatistics()
    {
        GlobalControl.Instance.SavedPlayerData = _localPlayerData;
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
