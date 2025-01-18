using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfoData", menuName = "custom/WeaponInfoData")]
public class WeaponInfo : ItemInfo{

    [SerializeField]
    string ammo;
    [SerializeField]
    float damage;
    [SerializeField]
    float bulleTspeed;
    [SerializeField]
    float fireRange;

}
