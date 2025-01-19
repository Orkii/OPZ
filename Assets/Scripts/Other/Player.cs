using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature, ISaveLoadable {
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

        ((ISaveLoadable)this).initISaveLoadable();
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

    void OnDestroy() {
        ((ISaveLoadable)this).destroyISaveLoadable();
    }

    public object load(XElement xml) {
        Debug.Log("player load");
        inventory = GetComponent<Inventory>();
        inventory.load(xml.Element("inventory"));
        transform.position = Utility.StringToVector3(xml.Element("position").Value);
        HP = float.Parse(xml.Element("HP").Value);
        return this;
    }

    public XElement save() {
        XElement ret = new XElement("player");
        ret.Add(new XElement("position", transform.position));
        ret.Add(new XElement("HP", HP));
        ret.Add(inventory.save());
        return ret;
    }
}


