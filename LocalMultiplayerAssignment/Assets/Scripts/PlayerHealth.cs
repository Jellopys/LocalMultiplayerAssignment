using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject _characterProfileGUI;
    private float _currentHealth;
    private float _maxHealth = 100f;
    private float _healthBarConversion;

    public delegate void DelegateTakeDamage(float health);
    public event DelegateTakeDamage UpdateHealthBar;

    void Start()
    {
        _currentHealth = _maxHealth;
        
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBarConversion = _currentHealth / _maxHealth;        

        if (UpdateHealthBar != null)
            UpdateHealthBar(_healthBarConversion);
    
        if (_currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " has died");
            TurnManager.GetInstance().CharacterDied(this.gameObject);
        }
    }

    public void InitializeUI(int playerIndex)
    {
        RectTransform profileContainer = UIManager.GetInstance().GetProfileContainer();

        Vector3 spawnLocation = new Vector3(profileContainer.localPosition.x, profileContainer.localPosition.y, profileContainer.localPosition.z);

        GameObject playerProfile = Instantiate(_characterProfileGUI, spawnLocation, Quaternion.identity);
        playerProfile.GetComponent<RectTransform>().SetParent(profileContainer);
        // playerProfile.GetComponent<CharacterProfileGUI>().Initialize(playerIndex, this);
    }
}
