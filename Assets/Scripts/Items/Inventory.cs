using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour, ISaveLoadable {
    public List<ItemInInventory> items = new List<ItemInInventory>();
    public delegate void InventoryHandler(List<ItemInInventory> list);
    public event InventoryHandler onChange;
    public event InventoryHandler onOpen;
    public event InventoryHandler onClose;



    InputAction inventoryOpenAction;
    InputAction inventoryCloseAction;
    InputAction deleteAction;

    PlayerControl control;

    ItemInInventory selectedItem;


    public int capacity { get; protected set; } = 15;

    private void Awake() {
        control = new PlayerControl();
    }


    void Start() {
        inventoryOpenAction     = control.PlayerInput.Openinventory;
        inventoryCloseAction    = control.InventoryInput.Closeinventory;
        deleteAction            = control.InventoryInput.Delete;

        inventoryOpenAction.performed   += open;
        inventoryCloseAction.performed  += close;
        deleteAction.performed          += deleteSelectedItem;

        control.InventoryInput.Enable();
        control.PlayerInput.Enable();
        inventoryOpenAction.Enable();
        inventoryCloseAction.Enable();
        deleteAction.Enable();

    }



    public bool setSelectedItem(ItemInInventory item) {
        if (items.Contains(item)) {
            if (selectedItem != null) selectedItem.selected = false;
            if (item != null) item.selected = true;
            selectedItem = item;
            return true;
        }
        return false;
    }

    public void deleteItem(ItemInInventory item) {
        items.Remove(item);
        onChange.Invoke(items);
    }
    public void deleteSelectedItem(InputAction.CallbackContext context) {
        deleteItem(selectedItem);
    }
    private void open(InputAction.CallbackContext context) {
        open();
    }
    public void open() {
        if (onOpen != null) onOpen.Invoke(items);
    }

    private void close(InputAction.CallbackContext context) {
        if (onClose != null) onClose.Invoke(items);
        foreach (ItemInInventory item in items) {
            item.selected = false;
        }
    }
    public void close() {
        if (onClose != null) onClose.Invoke(items);
    }

    public object load(XElement xml) {
        Debug.Log("Invent = " + xml);
        List<XElement> items_ = xml.Elements("item").ToList();
        capacity = int.Parse(xml.Element("capacity").Value);
        foreach (XElement ob in items_) {
            ItemInInventory iii = new ItemInInventory();
            iii.load(ob.Element("itemInInventory"));
            items.Add(iii);
        }
        return this;
    }
    public XElement save() {
        XElement root = new XElement("inventory");
        foreach (ISaveLoadable ob in items) {
            root.Add(new XElement("item", ob.save()));
        }
        root.Add(new XElement("capacity", capacity));
        return root;
    }

    public bool tryPutItem(ItemInWorld item) {
        Debug.Log("item floor name = " + item.itemInfo.itemName);
        foreach (ItemInInventory ob in items) {
            Debug.Log("item inventory name = " + ob.itemInfo.itemName);
            if (ob.itemInfo.itemName == item.itemInfo.itemName) {
                ob.count++;
                UnityEngine.Object.Destroy(item.gameObject);
                return true;
            }
        }

        if (items.Count >= capacity) return false;
        items.Add(new ItemInInventory(item.itemInfo));
        if (onChange != null) onChange.Invoke(items);
        UnityEngine.Object.Destroy(item.gameObject);
        return true;
    }





}