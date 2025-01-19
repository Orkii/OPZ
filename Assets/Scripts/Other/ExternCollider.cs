using UnityEngine;
using UnityEngine.U2D;
using static Eyes;
using static Inventory;

public class ExternCollider : MonoBehaviour {
    public event EyesHandler onEnter;
    public event EyesHandler onExit;

    //public Collider2D collider;


    private void Start() {
        //if (collider == null) collider = GetComponent<Cllider2D>();
    }




    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("OnTriggerEnter2D");
        if (onEnter != null) {
            onEnter.Invoke(col);
            // Debug.Log("OnTriggerEnter2D");
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        // Debug.Log("OnTriggerEnter2D");
        if (onExit != null) onExit.Invoke(col);
    }
}
