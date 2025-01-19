using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : MonoBehaviour {



    float speed;
    float damage;
    float fireRange;
    Vector2 dir;
    Rigidbody2D rb;
    bool shooted;

    Vector2 startPos;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (!shooted) return;
        if (new Vector2(
                - transform.position.x + startPos.x,
                - transform.position.y + startPos.y).magnitude>= fireRange) {
            
            Debug.Log("fireRange = " + fireRange);
            Debug.Log("AAAAAAAAA = " + new Vector2(
                transform.position.x - startPos.x,
                transform.position.y - startPos.y).magnitude);
            Destroy(gameObject);
        }
        rb.linearVelocity = dir * speed;
    }


    public void init(WeaponInfo weaponInfo) {
        speed = weaponInfo.bulleTspeed;
        damage = weaponInfo.damage;
        fireRange = weaponInfo.fireRange;
    }

    public void shoot(Vector2 dir_, Vector2 pos, float rotate) {
        dir = dir_;
        shooted = true;
        transform.rotation = Quaternion.Euler(0, 0, rotate - 90);
        transform.position = pos;
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Bullet Enter");
        Debug.Log("Bullet Enter tag = " + collision.tag);
        if (collision.tag == "Enemy") {
            collision.GetComponent<Enemy>().doDamage(damage);
            Destroy(gameObject);
        }
    }

}
