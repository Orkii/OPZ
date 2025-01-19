using UnityEngine;

public class Equipped : MonoBehaviour {
    [SerializeField]
    WeaponInfo weapon;
    [SerializeField]
    Eye eye;
    SpriteRenderer spriteRenderer;

    GameObject target;


    void Start() {
        Debug.Log("%W% START");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (weapon != null) setWeapon(weapon);
        //weapon.bullet.GetComponent<Bullet>().init(weapon);
        eye.onEnter += seeSMTH;
        eye.onExit += unSeeSMTH;
        eye.circleCollider.radius = weapon.fireRange;
    }
    Vector2 rotation;
    void Update() {
        rotation = (transform.rotation * Vector2.right).normalized;
        flip();
        //Debug.Log("Target = " + target);
        looAt(target);

    }



    protected void looAt(GameObject target) {
        float rotate = 0;
        if (target != null) {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            float x = Mathf.Acos(dir.x);
            float y = Mathf.Asin(dir.y);
            rotate = x * Mathf.Rad2Deg;
            if (y < 0) rotate *= -1;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }


    void setWeapon(WeaponInfo weapon_) {
        if (weapon != null) weapon = weapon_;
        spriteRenderer.sprite = weapon.image;
    }


    protected void flip() {
        if (rotation.x > 0) {
            if (transform.localScale.y < 0) {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            }
        }
        else {
            if (transform.localScale.y > 0) {
                transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
            }
        }
    }

    public void shoot() {
        GameObject b = GameObject.Instantiate(weapon.bullet);
        Bullet a = b.GetComponent<Bullet>();
        a.init(weapon);
        a.shoot(rotation, transform.position, transform.rotation.eulerAngles.z);
    }


    protected void seeSMTH(Collider2D collider) {
        if (collider.tag == "Enemy") {
            target = collider.gameObject;
        }
    }
    protected void unSeeSMTH(Collider2D collider) {
        if (collider.gameObject == target) {
            target = null;
        }
    }

}
