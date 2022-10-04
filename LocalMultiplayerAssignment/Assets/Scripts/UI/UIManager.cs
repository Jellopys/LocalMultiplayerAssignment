using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _characterProfileGUI;
    [SerializeField] GameObject _weaponUIPrefab;
    [SerializeField] Image progressBar;
    [SerializeField] Image progress;
    private static UIManager instance;
    private bool isChargingWeapon;
    private bool _reverse;

    [SerializeField] private RectTransform _profileContainer;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }
    }

    public static UIManager GetInstance()
    {
        return instance;
    }

    void Update()
    {
        if (isChargingWeapon)
        {
            
            if (_reverse == false && progress.fillAmount <= 1)
            {
                progress.fillAmount = progress.fillAmount + 1f * Time.deltaTime;

                if (progress.fillAmount >= 1)
                    _reverse = true;

            }
            else if (_reverse == true && progress.fillAmount >= 0)
            {
                progress.fillAmount = progress.fillAmount - 1f * Time.deltaTime;

                if (progress.fillAmount <= 0)
                    _reverse = false;
            }
        }
    }

    public void SetIsHolding(bool isCharging)
    {
        isChargingWeapon = isCharging;
        progressBar.enabled = isCharging;
        progress.enabled = isCharging;
        _reverse = false;
        if (!isCharging)
        {
            progress.fillAmount = 0;
        }
    }

    public RectTransform GetProfileContainer()
    {
        return _profileContainer;
    }

    public void InitPlayerProfiles(GameObject character, int i)
    {
        Vector3 spawnLocation = new Vector3(_profileContainer.localPosition.x, _profileContainer.localPosition.y, _profileContainer.localPosition.z);
        GameObject playerProfile = Instantiate(_characterProfileGUI, spawnLocation, Quaternion.identity);
        playerProfile.GetComponent<RectTransform>().SetParent(_profileContainer);
        playerProfile.GetComponent<CharacterProfileGUI>().Initialize(i, character.GetComponent<PlayerHealth>());
        character.GetComponent<WeaponManager>().SetWeaponsUI(playerProfile.transform, _weaponUIPrefab);
    }
}
