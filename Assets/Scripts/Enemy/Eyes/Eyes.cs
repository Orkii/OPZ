using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eyes : MonoBehaviour {
    public delegate void EyesHandler(Collider2D col);

    public event EyesHandler onSee;
    public event EyesHandler onUnSee;

    [SerializeField]
    Eye closeEye;
    [SerializeField]
    Eye farEye;


    private void Start() {
        //if (closeEye == null) 
        closeEye.onEnter += onSee_;
        //if (farEye == null) 
        farEye.onExit += onUnSee_;
    }

    protected void onSee_(Collider2D col) {
        Debug.Log("onSee_");
        onSee.Invoke(col);
    }
    protected void onUnSee_(Collider2D col) {
        Debug.Log("onUnSee_");
        onUnSee.Invoke(col);
    }


}
