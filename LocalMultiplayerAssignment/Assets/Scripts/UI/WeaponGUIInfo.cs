using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponGUIInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private Image _gunIcon;
    [SerializeField] private Image _keybindIcon;

    public void InitWeaponUI(int _currentAmmunition, Sprite weaponIcon, Sprite keybindIcon)
    {
        _ammoText.text = _currentAmmunition.ToString();
        _gunIcon.sprite = weaponIcon;
        _keybindIcon.sprite = keybindIcon;
    }

    public void UpdateAmmo(int _currentAmmunition)
    {
        _ammoText.text = _currentAmmunition.ToString();
    }
}
