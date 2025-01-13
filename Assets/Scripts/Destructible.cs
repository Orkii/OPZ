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

    void Start() {
        
    }

    void Update() {
        
    }

    public void doDamage(float damage) {
        HP -= damage;
        if (damage <= 0) {
            EventArgs args = new OnDieEventArgs(HP.ToString());
            onBeforeDie.Invoke(this, args);
            die();
            onAfterDie.Invoke(this, args);
        }
    }
    protected void die() {
        Destroy(gameObject);
    }
}
