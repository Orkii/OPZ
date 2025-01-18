using UnityEngine;
using UnityEngine.U2D;
using static Eyes;
using static Inventory;

public class Eye : MonoBehaviour {
    public event EyesHandler onEnter;
    public event EyesHandler onExit;

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("OnTriggerEnter2D");
        if (onEnter != null) {
            onEnter.Invoke(col);
            Debug.Log("OnTriggerEnter2D");
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("OnTriggerEnter2D");
        if (onExit!= null) onExit.Invoke(col);
    }
}
