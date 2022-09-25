using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileGUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _healthBar;
    private int _defaultPosition = 384;

    private PlayerHealth _playerHealth; 

    public void SetGUIPosition(int playerIndex)
    {
        Vector3 newLocation = new Vector3 (_defaultPosition * playerIndex, 0, 0);
        _rectTransform.position = newLocation;
    }

    public void Initialize(int playerIndex, PlayerHealth playerHealth)
    {
        SetGUIPosition(playerIndex);

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
