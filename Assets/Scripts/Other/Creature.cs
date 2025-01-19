using UnityEngine;

public class Creature : Destractable {

    protected Rigidbody2D rb;
    [SerializeField]
    protected float speed;
    override protected void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    override protected void Update() {
        base.Update();
    }
}
