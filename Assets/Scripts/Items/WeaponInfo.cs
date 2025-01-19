using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfoData", menuName = "custom/WeaponInfoData")]
public class WeaponInfo : ItemInfo{

    [SerializeField]
    public string ammo;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float bulleTspeed;
    [SerializeField]
    public float fireRange;
    [SerializeField]
    public GameObject bullet;
    public string bulletPath {
        get { return AssetDatabase.GetAssetPath(bullet); }
        set { bullet = AssetDatabase.LoadAssetAtPath<GameObject>(value); }
    }


    private void Awake() {
        bullet.GetComponent<Bullet>().init(this);
    }



}
