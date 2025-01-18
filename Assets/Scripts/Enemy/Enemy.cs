using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    CreatureInfo creatureInfo;
    [SerializeField]
    Eyes eyes;

    GameObject targetToGo;

    Rigidbody2D rb;
    
    
    public void onSawSmth(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            targetToGo = col.gameObject;
        }       
    }
    public void onUnSawSmth(Collider2D col) {
        targetToGo = null;
    }


    void Start() {
        if (eyes != null) {
            eyes.onSee += onSawSmth;
            eyes.onUnSee += onUnSawSmth;
        }
        rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (targetToGo != null) {
            Vector2 dir = targetToGo.transform.position - transform.position;
            Debug.Log("dir1 = " + dir);
            dir /= dir.magnitude;
            Debug.Log("dir2 = " + dir);
            rb.linearVelocity = dir * creatureInfo.speed;
        }
        else rb.linearVelocity = Vector2.zero;
    }


}
