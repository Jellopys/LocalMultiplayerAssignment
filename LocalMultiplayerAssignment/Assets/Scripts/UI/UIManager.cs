using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _characterProfileGUI;
    [SerializeField] private GameObject _weaponUIPrefab;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _progress;
    private static UIManager _instance;
    private bool _isChargingWeapon;
    private bool _reverse;

    [SerializeField] private RectTransform _profileContainer;
    
    public static UIManager GetInstance() { return _instance; }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(this); }
        else { _instance = this; }
    }

    void Update()
    {
        if (_isChargingWeapon)
        {
            if (_reverse == false && _progress.fillAmount <= 1)
            {
                _progress.fillAmount = _progress.fillAmount + 1f * Time.deltaTime;

                if (_progress.fillAmount >= 1)
                    _reverse = true;

            }
            else if (_reverse == true && _progress.fillAmount >= 0)
            {
                _progress.fillAmount = _progress.fillAmount - 1f * Time.deltaTime;

                if (_progress.fillAmount <= 0)
                    _reverse = false;
            }
        }
    }

    public void SetIsHolding(bool isCharging)
    {
        _isChargingWeapon = isCharging;
        _progressBar.enabled = isCharging;
        _progress.enabled = isCharging;
        _reverse = false;
        if (!isCharging)
        {
            _progress.fillAmount = 0;
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
