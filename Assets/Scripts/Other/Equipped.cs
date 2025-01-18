using UnityEngine;

public class Equipped : MonoBehaviour {
    [SerializeField]
    WeaponInfo weapon;

    SpriteRenderer spriteRenderer;

    void Start() {
        Debug.Log("%W% START");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (weapon != null) setWeapon(weapon);
        
    }
     
    void Update() {
        
    }
    
    void setWeapon(WeaponInfo weapon_) {
        if (weapon != null) weapon = weapon_;
        spriteRenderer.sprite = weapon.image;
    }


    public void shoot() {
        Debug.Log("BOOOOOM");
    }


}
