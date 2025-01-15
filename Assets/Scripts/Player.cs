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

    [SerializeField]
    protected InputActionReference movementInput;
    [SerializeField]
    protected InputActionReference shootInput;
    [SerializeField]
    protected InputActionReference inventoryInput;

    protected Inventory inventory = new Inventory();

    Vector2 moveV;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("move = " + moveV);
        //characterController.Move(move * speed * Time.deltaTime);
        rb.linearVelocity = moveV * speed;
    }
}


public class Inventory : ISaveLoadable {
    public List<ItemInInventory> items;
    int capacity = 5;
    public object load(XElement xml) {
        throw new System.NotImplementedException();
    }

    public XElement save() {
        XElement root = new XElement("inventory");
        foreach (ISaveLoadable ob in items) {
            root.Add(ob.save());
        }
        return root;
    }
}