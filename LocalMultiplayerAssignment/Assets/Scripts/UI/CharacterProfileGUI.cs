using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterProfileGUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _avatar;
    [SerializeField] private Sprite[] _playerAvatars;
    private List<IWeapon> _weaponList;
    private int _defaultPosition = 384;

    private PlayerHealth _playerHealth; 

    public void SetGUIPosition(int playerIndex)
    {
        Vector3 newLocation = new Vector3 (_defaultPosition * (playerIndex + 1), 0, 0);
        _rectTransform.position = newLocation;
    }

    public void Initialize(int playerIndex, PlayerHealth playerHealth)
    {
        SetGUIPosition(playerIndex);
        _avatar.sprite = _playerAvatars[playerIndex];
        InitPlayerHealth(playerHealth);
    }

    public void InitPlayerHealth(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerHealth.UpdateHealthBar += UpdateHealthBarGUI;
    }

    public void UpdateHealthBarGUI(float health)
    {
        _healthBar.fillAmount = health;
    }

    void OnDisable()
    {
        _playerHealth.UpdateHealthBar -= UpdateHealthBarGUI;
    }
}
