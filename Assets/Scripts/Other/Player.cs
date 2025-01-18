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
    //public float maxSpeed = 1;
    //public float acceleration = 1;
    //public float deceleration = 1;
    [SerializeField]
    Equipped equipped;
    protected Inventory inventory;

    InputAction moveAction;
    InputAction shootAction;
    InputAction inventoryOpenAction;

    InputAction inventoryCloseAction;

    PlayerControl playerControl;// = new PlayerControl();

    Vector2 moveV;


    private void OnEnable() {
        playerControl.PlayerInput.Enable();
    }
    private void OnDisable() {
        playerControl.PlayerInput.Disable();
    }

    private void Awake() {
        playerControl = new PlayerControl();
    }
    void Start() {
        moveAction              = playerControl.PlayerInput.Movement;
        shootAction             = playerControl.PlayerInput.Shoot;
        //inventoryOpenAction     = playerControl.PlayerInput.Openinventory;
        //inventoryCloseAction    = playerControl.InventoryInput.Closeinventory;

        shootAction.performed           += fire;
        //inventoryOpenAction.performed   += openInventory;
        //inventoryCloseAction.performed  += onCloseInventory;

        playerControl.PlayerInput.Enable();
        //playerControl.InventoryInput.Enable();

        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void Update() {
        move();
    }


    protected void openInventory(InputAction.CallbackContext context) {
        //inventory.open();
        //playerControl.PlayerInput.Disable();
    }
    protected void onCloseInventory(InputAction.CallbackContext context) {
        //inventory.close();
        //playerControl.PlayerInput.Enable();
    }

    protected void move() {
        moveV = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = moveV * speed;
    }
    protected void fire(InputAction.CallbackContext context) {
        equipped.shoot();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Enter " + collision.name);
        if (isItemInWorld(collision)) return;
    }
    protected bool isItemInWorld(Collider2D colider) {
        ItemInWorld item = colider.gameObject.GetComponent<ItemInWorld>();
        if (item == null) return false;
        inventory.tryPutItem(item);
        
        return true;
    }

}


