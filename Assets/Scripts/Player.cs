using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature {

    private Rigidbody2D rb;
    [SerializeField]
    public float speed = 1;
    public float maxSpeed = 1;
    public float acceleration = 1;
    public float deceleration = 1;
    protected Inventory inventory;

    [SerializeField]
    protected InputActionReference movementInput;
    [SerializeField]
    protected InputActionReference shootInput;
    [SerializeField]
    protected InputActionReference inventoryInput;


    Vector2 moveV;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        inventoryInput.action.performed += openInventory;
        movementInput.action.Enable();
        shootInput.action.Enable();
        inventoryInput.action.Enable();
    }

    void Update() {
        move();


        float shoot = shootInput.action.ReadValue<float>();
        Debug.Log("shoot= " + shoot);
        if (shoot == 1) {
            SaveLoader.save();
            string myPath = AssetDatabase.GetAssetPath(gameObject);
            Debug.Log("myPath = " + myPath);
            SaveLoader.save();
        }
    }


    protected void openInventory(InputAction.CallbackContext obj) {
        movementInput.action.Disable();
        shootInput.action.Disable();
        inventoryInput.action.Disable();
    }
    protected void move() {
        moveV = movementInput.action.ReadValue<Vector2>();
        //Debug.Log("move = " + moveV);
        //characterController.Move(move * speed * Time.deltaTime);
        rb.linearVelocity = moveV * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Touch = " + collision.name);
        if (isItemInWorld(collision)) return;
    }
    protected bool isItemInWorld(Collider2D colider) {
        ItemInWorld item = colider.gameObject.GetComponent<ItemInWorld>();
        if (item == null) return false;
        inventory.tryPutItem(item);
        
        return true;
    }

}


