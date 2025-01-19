using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature {   
    [SerializeField]
    Equipped equipped;
    protected Inventory inventory;
    InputAction moveAction;
    InputAction shootAction;
    PlayerControl playerControl;
    [SerializeField]
    ExternCollider collector;
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
    override protected void Start() {
        base.Start();
        moveAction              = playerControl.PlayerInput.Movement;
        shootAction             = playerControl.PlayerInput.Shoot;
        shootAction.performed           += fire;
        playerControl.PlayerInput.Enable();
        inventory = GetComponent<Inventory>();
        collector.onEnter += OnCollectorEnter;
    }

    override protected void Update() {
        base.Update();
        move();
    }

    /*
    protected void openInventory(InputAction.CallbackContext context) {
        //inventory.open();
        //playerControl.PlayerInput.Disable();
    }
    protected void onCloseInventory(InputAction.CallbackContext context) {
        //inventory.close();
        //playerControl.PlayerInput.Enable();
    }
    */
    protected void move() {
        moveV = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = moveV * speed;
    }
    protected void fire(InputAction.CallbackContext context) {
        equipped.shoot();
    }
    private void OnCollectorEnter(Collider2D collision) {
        if (isItemInWorld(collision)) return;
    }
    protected bool isItemInWorld(Collider2D colider) {
        ItemInWorld item = colider.gameObject.GetComponent<ItemInWorld>();
        if (item == null) return false;
        inventory.tryPutItem(item);
        
        return true;
    }

}


