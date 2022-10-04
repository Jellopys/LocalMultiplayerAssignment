using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Shoot(bool isHolding);
    void Reload();
    void SetupWeaponUI(Transform playerProfileGUI, GameObject weaponUIPrefab);
}
