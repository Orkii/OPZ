using System;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;
public class OnDieEventArgs : EventArgs {
    public OnDieEventArgs(string text) { 
        Text = text; 
    }
    public string Text { get; } // readonly
}

public class Destractable : MonoBehaviour {

    [SerializeField]
    protected float HP;

    public event EventHandler onBeforeDie;
    public event EventHandler onAfterDie;

    virtual protected void Start() {
        
    }

    virtual protected void Update() {
        
    }

    public void doDamage(float damage) {
        HP -= damage;
        if (HP <= 0) {
            EventArgs args = new OnDieEventArgs(HP.ToString());
            if (onBeforeDie != null) onBeforeDie.Invoke(this, args);
            die();
            //if (onAfterDie != null) onAfterDie.Invoke(this, args);
        }
    }
    protected void die() {
        Destroy(gameObject);
    }
}
