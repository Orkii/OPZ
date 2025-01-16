using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour, ISaveLoadable {
    public List<ItemInInventory> items = new List<ItemInInventory>();
    int capacity = 5;
    
    public Inventory() { 

    }

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

    XElement ISaveLoadable.save() {
        throw new System.NotImplementedException();
    }

    public bool tryPutItem(ItemInWorld item) {
        if (items.Count >= capacity) return false;
        items.Add(new ItemInInventory(item.itemInfo));
        if (onChange != null) onChange.Invoke(items);
        UnityEngine.Object.Destroy(item.gameObject);
        return true;
    }




    public delegate void InventoryHandler(List<ItemInInventory> list);
    public event InventoryHandler onChange;
}